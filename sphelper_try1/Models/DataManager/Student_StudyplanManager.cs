using System.Collections.Generic;
using System.Linq;

namespace sphelper_try1.Models.DataManager
{
    public class Student_StudyplanManager
    {
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
                    Where(x => x.QualCode == qualCode && x.Priority == 1);
                string studyplan = query.First().StudyPlanCode;

                return studyplan;
            }
        }

        public static string FindStudentGrade(string studentId, int termCode, int termYear, string subjectCode)
        {
            using (SPHelperEntities context = new SPHelperEntities())
            {
                var query = from student_grade in context.student_grade
                            join crn_detail in context.crn_detail
                            on student_grade.CRN equals crn_detail.CRN
                            where student_grade.StudentID == studentId &&
                                  student_grade.TermCode <= termCode &&
                                  student_grade.TermYear <= termYear && 
                                  crn_detail.SubjectCode == subjectCode
                            select student_grade.Grade;
                
                var status = "";
                var grade = query.FirstOrDefault();

                if (grade == null)
                {
                    status = "N/A";
                } else {
                    status = grade;
                } 

                return status;
            }
        }

        
    }
}