using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace sphelper_try1.Models.DataManager
{
    public class Student_StudyplanManager
    {
        //public static studentinfo 
        public static string FindstudentName(string studentId)
        {
            using (SPHelperEntities context = new SPHelperEntities())
            {
                var query = from s in context.students
                            where s.StudentID == studentId
                            select s.GivenName;

                return query.FirstOrDefault().ToString();
            }
        }

        public static qualification FindQualificationByStudentId(string studentId)
        {
            using (SPHelperEntities context = new SPHelperEntities())
            {
                var query = from student_studyplan in context.student_studyplan
                            join qual in context.qualifications
                            on student_studyplan.QualCode equals qual.QualCode
                            where student_studyplan.StudentID == studentId
                            select new
                            {
                                student_studyplan.QualCode,
                                qual.QualName,
                                qual.NationalQualCode,
                                qual.TafeQualCode

                            };

                var qualification = new qualification()
                {
                    QualCode = query.First().QualCode,
                    QualName = query.First().QualName,
                    NationalQualCode = query.First().NationalQualCode,
                    TafeQualCode = query.First().TafeQualCode
                };

                return qualification;
            }
        }

        public static SemesterNow GetCurrentSemester(DateTime dateNow)
        {
            int semester = 0;
            int year = dateNow.Year;

            //get semester
            if (dateNow.Month >= 1 && dateNow.Month <= 6)
            {
                semester = 1;
            }
            else if (dateNow.Month >=7 && dateNow.Month <=12)
            {
                semester = 2;
            }

            var semesterNow = new SemesterNow()
            {
                Semester = semester,
                //Year = year
                Year = 2019
            };

            return semesterNow;
        }

        public static qualification FindQualificationByQualCode(string qualCode)
        {
            using (SPHelperEntities context = new SPHelperEntities())
            {
                var query = from q in context.qualifications.ToList()
                            where q.QualCode == qualCode
                            select new 
                            {
                                q.QualCode,
                                q.QualName,
                                q.NationalQualCode,
                                q.TafeQualCode
                            };

                qualification qualification = new qualification()
                {
                    QualCode = query.First().QualCode,
                    QualName = query.First().QualName,
                    NationalQualCode = query.First().NationalQualCode,
                    TafeQualCode = query.First().TafeQualCode
                };

                return qualification;
            }
        }

        public static string FindStudyplanByQualificaitonCode(string qualCode)
        {
            using (SPHelperEntities context = new SPHelperEntities())
            {
                var query = context.studyplan_qualification.
                    Where(x => x.QualCode == qualCode && x.Priority == 2);

                string studyPlanCode = query.First().StudyPlanCode;

                return studyPlanCode;
            }
        }

        public static List<StudyPlanSubjects> GetStudyPlanByStudentId(string studyPlanCode)
        {
            using (SPHelperEntities context = new SPHelperEntities())
            {
                var query = from subj in context.subjects.ToList()
                            join sp_subj in context.studyplan_subject.ToList()
                            on subj.SubjectCode equals sp_subj.SubjectCode
                            orderby sp_subj.TimingSemester, sp_subj.TimingSemesterTerm
                            where sp_subj.StudyPlanCode == studyPlanCode
                            select new StudyPlanSubjects()
                            {
                                Semester = sp_subj.TimingSemester,
                                SubjectCode = subj.SubjectCode,
                                SubjectTitle = subj.SubjectDescription,
                                SubjectDescription = subj.SubjectLongDescription
                            };

                return query.ToList();
            }
        }

        public static List<SubjectResults> FindStudentGradeList(string studentId)
        {
            using (SPHelperEntities context = new SPHelperEntities())
            {
                var query = from student_grade in context.student_grade
                            join crn_detail in context.crn_detail
                            on student_grade.CRN equals crn_detail.CRN
                            where student_grade.StudentID == studentId &&
                                  student_grade.TafeCompCode == crn_detail.TafeCompCode
                            select new SubjectResults() {
                                SubjectCode = crn_detail.SubjectCode,
                                Grade = student_grade.Grade
                            };

                return query.ToList();
            }
        }

        public static string FindSubjectGrade(string subjectCode)
        {
            var studentinfo = MemoryCache.Default["studentinfo"] as studentinfo;
            var results = studentinfo.SubjectResultsList;
            var grade = results.Where(x => x.SubjectCode == subjectCode).Select(x => x.Grade).FirstOrDefault();

            if (grade != null)
            {
                return grade;
            }
            else
            {
                grade = "N/A";
                return grade;
            }
        }

        public static List<string> FindPrerequisite(string subjectCode) 
        {
            var studentinfo = MemoryCache.Default["studentinfo"] as studentinfo;
            var prerequisiteList = new List<string>();
            using (SPHelperEntities context = new SPHelperEntities())
            {
                var query = from prereq_comb in context.prerequisite_combination.ToList()
                            join prereq in context.prerequisites.ToList() on prereq_comb.Id equals prereq.Id
                            where prereq_comb.SubjectCode == subjectCode && prereq_comb.StudyPlanCode == studentinfo.StudyPlanCode
                            select prereq.SubjectCode;

                var result = query.ToList();

                if (result.Count > 0)  
                {
                    prerequisiteList.AddRange(result);
                }
                else
                {
                    prerequisiteList.Add("N/A");
                }
                
                return prerequisiteList;
            }
        }

        public static string FindCombinationKey(string subjectCode)
        {
            var studentinfo = MemoryCache.Default["studentinfo"] as studentinfo;
            using (SPHelperEntities context = new SPHelperEntities())
            {
                var query = from prereq_comb in context.prerequisite_combination.ToList()
                            join prereq in context.prerequisites.ToList() on prereq_comb.Id equals prereq.Id
                            where prereq_comb.SubjectCode == subjectCode && prereq_comb.StudyPlanCode == studentinfo.StudyPlanCode
                            select prereq_comb.Combination_Id;

                return query.FirstOrDefault();
            }
        }

        public static bool IsPrerequisiteCompleted(string subjectCode)
        {
            var studentinfo = MemoryCache.Default["studentinfo"] as studentinfo;
            var resultsList = studentinfo.SubjectResultsList;
            var resultedSubjectCodeList = resultsList.Select(x => x.SubjectCode).ToList();
            var gradesList = new List<string>();
            var result = false;
            var finalResult = false;
            List<bool> isPass = new List<bool>();


            var prereqSubjectCodeList = FindPrerequisite(subjectCode);

            if(!prereqSubjectCodeList.Contains("N/A"))
            {
                var combination = FindCombinationKey(subjectCode);

                foreach (var prereqSubject in prereqSubjectCodeList)
                {
                    if (!resultedSubjectCodeList.Contains(prereqSubject)) //if sub
                    {
                        return finalResult;
                    }
                    else
                    {
                        var grade = resultsList.Where(x => x.SubjectCode == prereqSubject).Select(x => x.Grade).FirstOrDefault();
                        switch (grade)
                        {
                            case "D":
                                result = true;
                                break;
                            case "C":
                                result = true;
                                break;
                            case "P":
                                result = true;
                                break;
                            case "PA":
                                result = true;
                                break;
                            case "AP":
                                result = true;
                                break;
                            case "VP":
                                result = true;
                                break;
                            case "F":
                                result = false; //F
                                break;
                            case "MS":
                                result = false; //F
                                break;
                            case "OB":
                                result = true;
                                break;
                            case "RP":
                                result = true;
                                break;
                            case "RF":
                                result = true;
                                break;
                            case "RW":
                                result = true;
                                break;
                            case "ST":
                                result = true;
                                break;
                            case "W":
                                result = false; //F
                                break;
                            case "X":
                                result = true;
                                break;
                            case "XW":
                                result = false; //F
                                break;
                            case "N/A":
                                result = false;
                                break;
                            default:
                                result = false;
                                break;
                        }
                        gradesList.Add(grade);
                        isPass.Add(result);
                    }
                    
                }

                if (!gradesList.Contains("N/A")) //if it has results, check combination operator
                {
                    if (combination == "S") //single 
                    {
                        if (isPass.FirstOrDefault() != false)
                        {
                            finalResult = true;
                        }
                        else
                        {
                            return finalResult;
                        }
                    }
                    else if (combination == "A") //all results should pass should return true
                    {
                        if (isPass.Contains(false))
                        {
                            finalResult = false;
                        }
                    }
                    else //atleast one results should return true
                    {
                        if (isPass.Contains(true))
                        {
                            finalResult = true;
                        }
                    }
                }
                else //if doesnt have results
                {
                    finalResult = false;
                }
            }
            else //else theres no pre req
            {
                finalResult = true;
            }
            
            return finalResult;
        }
    }
}