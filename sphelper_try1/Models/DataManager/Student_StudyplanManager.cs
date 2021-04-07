using System.Collections.Generic;
using System.Linq;

namespace sphelper_try1.Models.DataManager
{
    public class Student_StudyplanManager
    {
        public static student FindStudentById(string studentId)
        {
            using (SpsDatabaseEntities context = new SpsDatabaseEntities())
            {
                var query = context.students.Where(x => x.StudentID == studentId);
                student student1 = new student()
                {
                    StudentID = query.First().StudentID,
                    GivenName = query.First().GivenName,
                    LastName = query.First().LastName,
                    EmailAddress = query.First().EmailAddress
                };
                return student1;
            }
        }

        public static qualification FindQualificationByStudentId(string studentId)
        {

            using (SpsDatabaseEntities context = new SpsDatabaseEntities())
            {
                var query = from student_studyplan in context.student_studyplan
                            join qualification in context.qualifications
                            on student_studyplan.QualCode equals qualification.QualCode
                            where student_studyplan.StudentID == studentId
                            select new
                            {
                                qualification.QualCode,
                                qualification.QualName,
                                qualification.NationalQualCode,
                                qualification.TafeQualCode

                            };
                qualification student_qualification = new qualification()
                {
                    QualCode = query.First().QualCode,
                    QualName = query.First().QualName,
                    NationalQualCode = query.First().NationalQualCode,
                    TafeQualCode = query.First().TafeQualCode
                };

                return student_qualification;
            }
        }

        public static string FindStudyplanByQualificaitonCode(string qualCode)
        {
            using (SpsDatabaseEntities context = new SpsDatabaseEntities())
            {
                var query = context.studyplan_qualification.Where(x => x.QualCode == qualCode && x.Priority == 1);
                string studyplan = query.First().StudyPlanCode;

                return studyplan;
            }
        }

        public static List<TableItems> GetAllSubjectByStudyplan(string qualification)
        {
            using (SpsDatabaseEntities context = new SpsDatabaseEntities())
            {
                var query = from studyplan_subject in context.studyplan_subject
                             join subject subject in context.subjects
                             on studyplan_subject.SubjectCode equals subject.SubjectCode
                             where studyplan_subject.StudyPlanCode == qualification //qualification string
                             orderby studyplan_subject.TimingSemester, studyplan_subject.TimingSemesterTerm
                             select new TableItems {
                                 SubjectCode =  subject.SubjectCode,
                                 SubjectTitle = subject.SubjectDescription,
                                 SubjectDescription = subject.SubjectLongDescription,
                                 Semester = studyplan_subject.TimingSemester.ToString(),
                                 TermSemester = studyplan_subject.TimingSemesterTerm.ToString()
                             };
                return query.ToList();

            }
        }
    }
}