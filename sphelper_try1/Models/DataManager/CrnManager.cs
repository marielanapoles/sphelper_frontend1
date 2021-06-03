using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace sphelper_try1.Models.DataManager
{
    public class CrnManager
    {
        public static List<subject_competency> GetTafeComptencyCodeBySubjectCodeQualCode(List<string> subjectCodes, string qualcode)
        {
            //inititially create a list
            var tafeCompetencySubjectCode = new List<subject_competency>();

            using (SPHelperEntities context = new SPHelperEntities())
            {
                foreach(var subject in subjectCodes)
                {
                    var query = from competencyQualification in context.competency_qualification.ToList()
                                join comptency in context.competencies.ToList()
                                on competencyQualification.NationalCompCode equals comptency.NationalCompCode
                                join subjectCompetency in context.subject_competency.ToList()
                                on comptency.TafeCompCode equals subjectCompetency.TafeCompCode
                                where subjectCompetency.SubjectCode == subject &&
                                      competencyQualification.QualCode == qualcode
                                //(competencyQualification.QualCode == qualcode || 
                                // competencyQualification.QualCode.Contains("PRG")) //hardcoded bc idk 
                                select new subject_competency()
                                {
                                    SubjectCode = subjectCompetency.SubjectCode,
                                    TafeCompCode = subjectCompetency.TafeCompCode,
                                    competency = subjectCompetency.competency,
                                    subject = subjectCompetency.subject
                                };

                    query.ToList();
                    tafeCompetencySubjectCode.AddRange(query);
                }
                return tafeCompetencySubjectCode;
            };
        }

        public static List<TableItemsCrn> GetCrn_Details(List<subject_competency> tafeCompSubjCode, int termCodeStart, int termYearStart)
        {
            var crnDetailsList = new List<TableItemsCrn>();

            using (SPHelperEntities context = new SPHelperEntities())
            {
                foreach(var item in tafeCompSubjCode)
                {
                    var query = from crndetails in context.crn_detail
                                join competency in context.competencies
                                on crndetails.TafeCompCode equals competency.TafeCompCode
                                where crndetails.TafeCompCode == item.TafeCompCode &&
                                      crndetails.SubjectCode == item.SubjectCode &&
                                      (crndetails.TermCodeStart >= termCodeStart && crndetails.TermCodeStart <= termCodeStart+1) &&
                                      crndetails.TermYearStart == termYearStart
                                select new TableItemsCrn()
                                {
                                    CRN = crndetails.CRN,
                                    SubjectCode = crndetails.SubjectCode,
                                    TafeCompetencyCode = competency.TafeCompCode,
                                    CompetencyName = competency.CompetencyName,
                                    isDeleted = false
                                };

                    var result = query.FirstOrDefault();


                    if ( result != null)
                    {
                        crnDetailsList.Add(result);
                    }
                    else
                    {
                        var nullResult = new TableItemsCrn()
                        {
                            CRN = "N/A",
                            SubjectCode = item.SubjectCode,
                            TafeCompetencyCode = item.TafeCompCode,
                            CompetencyName = item.competency.CompetencyName
                        };
                        crnDetailsList.Add(nullResult);
                    }
                }
                return crnDetailsList;
            }
        }

        
    }
}