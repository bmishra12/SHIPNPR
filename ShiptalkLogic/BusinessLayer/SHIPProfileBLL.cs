using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using ShiptalkLogic.DataLayer;
using AutoMapper;

namespace ShiptalkLogic.BusinessLayer
{
    public class SHIPProfileBLL
    {
        public SHIPProfileBLL()
        {
            CreateMaps();
        }


        public ViewSHIPProfileViewData GetSHIPProfile(string Id)
        {
            //Fill SHIP Profile here.
            var shippro = new SHIPProfileDAL().GetSHIPProfile(Id);

            return Mapper.Map<SHIPProfile, ViewSHIPProfileViewData>(shippro);
            
        }
        public EditShipProfileViewData GetEditSHIPProfile(string Id)
        {
            //Fill SHIP Profile here.
            var shippro = new SHIPProfileDAL().GetSHIPProfile(Id);

            return Mapper.Map<SHIPProfile, EditShipProfileViewData>(shippro);

        }

           private void CreateMaps()
        {
            Mapper.CreateMap<SHIPProfile, ViewSHIPProfileViewData >()

                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.AdminAgencyAddress, opt => opt.MapFrom(src => src.AdminAgencyAddress))
                .ForMember(dest => dest.AdminAgencyCity, opt => opt.MapFrom(src => src.AdminAgencyCity))
                .ForMember(dest => dest.AdminAgencyContactName, opt => opt.MapFrom(src => src.AdminAgencyContactName))
                .ForMember(dest => dest.AdminAgencyContactTitle, opt => opt.MapFrom(src => src.AdminAgencyContactTitle))
                .ForMember(dest => dest.AdminAgencyEmail, opt => opt.MapFrom(src => src.AdminAgencyEmail))
                .ForMember(dest => dest.AdminAgencyFax, opt => opt.MapFrom(src => src.AdminAgencyFax))
                .ForMember(dest => dest.AdminAgencyName, opt => opt.MapFrom(src => src.AdminAgencyName))
                .ForMember(dest => dest.AdminAgencyPhone, opt => opt.MapFrom(src => src.AdminAgencyPhone))
                .ForMember(dest => dest.AdminAgencyZipcode, opt => opt.MapFrom(src => src.AdminAgencyZipcode))
                .ForMember(dest => dest.AvailableLanguages, opt => opt.MapFrom(src => src.AvailableLanguages))
                .ForMember(dest => dest.BeneficiaryContactEmail, opt => opt.MapFrom(src => src.BeneficiaryContactEmail))
                .ForMember(dest => dest.BeneficiaryContactHours, opt => opt.MapFrom(src => src.BeneficiaryContactHours))
                .ForMember(dest => dest.BeneficiaryContactPhoneTollFree, opt => opt.MapFrom(src => src.BeneficiaryContactPhoneTollFree))
                .ForMember(dest => dest.BeneficiaryContactPhoneTollFreeInStateOnly, opt => opt.MapFrom(src => src.BeneficiaryContactPhoneTollFreeInStateOnly))
                .ForMember(dest => dest.BeneficiaryContactPhoneTollLine, opt => opt.MapFrom(src => src.BeneficiaryContactPhoneTollLine))
                .ForMember(dest => dest.BeneficiaryContactTDDLine, opt => opt.MapFrom(src => src.BeneficiaryContactTDDLine))
                .ForMember(dest => dest.BeneficiaryContactWebsite, opt => opt.MapFrom(src => src.BeneficiaryContactWebsite))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.MapFrom(src => src.LastUpdatedDate))
                .ForMember(dest => dest.ProgramCoordinatorAddress, opt => opt.MapFrom(src => src.ProgramCoordinatorAddress))
                .ForMember(dest => dest.ProgramCoordinatorCity, opt => opt.MapFrom(src => src.ProgramCoordinatorCity))
                .ForMember(dest => dest.ProgramCoordinatorEmail, opt => opt.MapFrom(src => src.ProgramCoordinatorEmail))
                .ForMember(dest => dest.ProgramCoordinatorFax, opt => opt.MapFrom(src => src.ProgramCoordinatorFax))
                .ForMember(dest => dest.ProgramCoordinatorPhone, opt => opt.MapFrom(src => src.ProgramCoordinatorPhone))
                .ForMember(dest => dest.ProgramCoordinatorName, opt => opt.MapFrom(src => src.ProgramCoordinatorName))
                .ForMember(dest => dest.ProgramCoordinatorZipcode, opt => opt.MapFrom(src => src.ProgramCoordinatorZipcode))
                .ForMember(dest => dest.ProgramName, opt => opt.MapFrom(src => src.ProgramName))
                .ForMember(dest => dest.ProgramSummary, opt => opt.MapFrom(src => src.ProgramSummary))
                .ForMember(dest => dest.ProgramWebsite, opt => opt.MapFrom(src => src.ProgramWebsite))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.NumberOfVolunteerCounselors, opt => opt.MapFrom(src => src.NumberOfVolunteerCounselors))
                .ForMember(dest => dest.NumberOfStateStaff, opt => opt.MapFrom(src => src.NumberOfStateStaff))
                .ForMember(dest => dest.TotalCounties, opt => opt.MapFrom(src => src.TotalCounties))                
                .ForMember(dest => dest.NumberOfCountiesServed, opt => opt.MapFrom(src => src.NumberOfCountiesServed))
                .ForMember(dest => dest.NumberOfSponsors, opt => opt.MapFrom(src => src.NumberOfSponsors))
                //New fields: added by Lavanya Maram - 07/23/2013
               .ForMember(dest => dest.StateOversightAgency, opt => opt.MapFrom(src => src.StateOversightAgency))
               .ForMember(dest => dest.NumberOfPaidStaff, opt => opt.MapFrom(src => src.NumberOfPaidStaff))
               .ForMember(dest => dest.NumberOfCoordinators, opt => opt.MapFrom(src => src.NumberOfCoordinators))
               .ForMember(dest => dest.NumberOfCertifiedCounselors, opt => opt.MapFrom(src => src.NumberOfCertifiedCounselors))
               .ForMember(dest => dest.NumberOfEligibleBeneficiaries, opt => opt.MapFrom(src => src.NumberOfEligibleBeneficiaries))
               .ForMember(dest => dest.NumberOfBeneficiaryContacts, opt => opt.MapFrom(src => src.NumberOfBeneficiaryContacts))
               .ForMember(dest => dest.LocalAgencies, opt => opt.MapFrom(src => src.LocalAgencies))
               .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
               .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude)); ;

            Mapper.CreateMap<SHIPProfile, EditShipProfileViewData>()

             .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
             .ForMember(dest => dest.AdminAgencyAddress, opt => opt.MapFrom(src => src.AdminAgencyAddress))
             .ForMember(dest => dest.AdminAgencyCity, opt => opt.MapFrom(src => src.AdminAgencyCity))
             .ForMember(dest => dest.AdminAgencyContactName, opt => opt.MapFrom(src => src.AdminAgencyContactName))
             .ForMember(dest => dest.AdminAgencyContactTitle, opt => opt.MapFrom(src => src.AdminAgencyContactTitle))
             .ForMember(dest => dest.AdminAgencyEmail, opt => opt.MapFrom(src => src.AdminAgencyEmail))
             .ForMember(dest => dest.AdminAgencyFax, opt => opt.MapFrom(src => src.AdminAgencyFax))
             .ForMember(dest => dest.AdminAgencyName, opt => opt.MapFrom(src => src.AdminAgencyName))
             .ForMember(dest => dest.AdminAgencyPhone, opt => opt.MapFrom(src => src.AdminAgencyPhone))
             .ForMember(dest => dest.AdminAgencyZipcode, opt => opt.MapFrom(src => src.AdminAgencyZipcode))
             .ForMember(dest => dest.AvailableLanguages, opt => opt.MapFrom(src => src.AvailableLanguages))
             .ForMember(dest => dest.BeneficiaryContactEmail, opt => opt.MapFrom(src => src.BeneficiaryContactEmail))
             .ForMember(dest => dest.BeneficiaryContactHours, opt => opt.MapFrom(src => src.BeneficiaryContactHours))
             .ForMember(dest => dest.BeneficiaryContactPhoneTollFree, opt => opt.MapFrom(src => src.BeneficiaryContactPhoneTollFree))
             .ForMember(dest => dest.BeneficiaryContactPhoneTollFreeInStateOnly, opt => opt.MapFrom(src => src.BeneficiaryContactPhoneTollFreeInStateOnly))
             .ForMember(dest => dest.BeneficiaryContactPhoneTollLine, opt => opt.MapFrom(src => src.BeneficiaryContactPhoneTollLine))
             .ForMember(dest => dest.BeneficiaryContactTDDLine, opt => opt.MapFrom(src => src.BeneficiaryContactTDDLine))
             .ForMember(dest => dest.BeneficiaryContactWebsite, opt => opt.MapFrom(src => src.BeneficiaryContactWebsite))
             .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
             .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
             .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
             .ForMember(dest => dest.LastUpdatedDate, opt => opt.MapFrom(src => src.LastUpdatedDate))
             .ForMember(dest => dest.NumberOfCountiesServed, opt => opt.MapFrom(src => src.NumberOfCountiesServed))
             .ForMember(dest => dest.NumberOfSponsors, opt => opt.MapFrom(src => src.NumberOfSponsors))
             .ForMember(dest => dest.NumberOfStateStaff, opt => opt.MapFrom(src => src.NumberOfStateStaff))
             .ForMember(dest => dest.NumberOfVolunteerCounselors, opt => opt.MapFrom(src => src.NumberOfVolunteerCounselors))
             .ForMember(dest => dest.ProgramCoordinatorAddress, opt => opt.MapFrom(src => src.ProgramCoordinatorAddress))
             .ForMember(dest => dest.ProgramCoordinatorCity, opt => opt.MapFrom(src => src.ProgramCoordinatorCity))
             .ForMember(dest => dest.ProgramCoordinatorEmail, opt => opt.MapFrom(src => src.ProgramCoordinatorEmail))
             .ForMember(dest => dest.ProgramCoordinatorFax, opt => opt.MapFrom(src => src.ProgramCoordinatorFax))
             .ForMember(dest => dest.ProgramCoordinatorPhone, opt => opt.MapFrom(src => src.ProgramCoordinatorPhone))
             .ForMember(dest => dest.ProgramCoordinatorName, opt => opt.MapFrom(src => src.ProgramCoordinatorName))
             .ForMember(dest => dest.ProgramCoordinatorZipcode, opt => opt.MapFrom(src => src.ProgramCoordinatorZipcode))
             .ForMember(dest => dest.ProgramName, opt => opt.MapFrom(src => src.ProgramName))
             .ForMember(dest => dest.ProgramSummary, opt => opt.MapFrom(src => src.ProgramSummary))
             .ForMember(dest => dest.ProgramWebsite, opt => opt.MapFrom(src => src.ProgramWebsite))
             .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
             .ForMember(dest => dest.TotalCounties, opt => opt.MapFrom(src => src.TotalCounties));


            Mapper.CreateMap<EditShipProfileViewData, SHIPProfile>()
             .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
             .ForMember(dest => dest.AdminAgencyAddress, opt => opt.MapFrom(src => src.AdminAgencyAddress))
             .ForMember(dest => dest.AdminAgencyCity, opt => opt.MapFrom(src => src.AdminAgencyCity))
             .ForMember(dest => dest.AdminAgencyContactName, opt => opt.MapFrom(src => src.AdminAgencyContactName))
             .ForMember(dest => dest.AdminAgencyContactTitle, opt => opt.MapFrom(src => src.AdminAgencyContactTitle))
             .ForMember(dest => dest.AdminAgencyEmail, opt => opt.MapFrom(src => src.AdminAgencyEmail))
             .ForMember(dest => dest.AdminAgencyFax, opt => opt.MapFrom(src => src.AdminAgencyFax))
             .ForMember(dest => dest.AdminAgencyName, opt => opt.MapFrom(src => src.AdminAgencyName))
             .ForMember(dest => dest.AdminAgencyPhone, opt => opt.MapFrom(src => src.AdminAgencyPhone))
             .ForMember(dest => dest.AdminAgencyZipcode, opt => opt.MapFrom(src => src.AdminAgencyZipcode))
             .ForMember(dest => dest.AvailableLanguages, opt => opt.MapFrom(src => src.AvailableLanguages))
             .ForMember(dest => dest.BeneficiaryContactEmail, opt => opt.MapFrom(src => src.BeneficiaryContactEmail))
             .ForMember(dest => dest.BeneficiaryContactHours, opt => opt.MapFrom(src => src.BeneficiaryContactHours))
             .ForMember(dest => dest.BeneficiaryContactPhoneTollFree, opt => opt.MapFrom(src => src.BeneficiaryContactPhoneTollFree))
             .ForMember(dest => dest.BeneficiaryContactPhoneTollFreeInStateOnly, opt => opt.MapFrom(src => src.BeneficiaryContactPhoneTollFreeInStateOnly))
             .ForMember(dest => dest.BeneficiaryContactPhoneTollLine, opt => opt.MapFrom(src => src.BeneficiaryContactPhoneTollLine))
             .ForMember(dest => dest.BeneficiaryContactTDDLine, opt => opt.MapFrom(src => src.BeneficiaryContactTDDLine))
             .ForMember(dest => dest.BeneficiaryContactWebsite, opt => opt.MapFrom(src => src.BeneficiaryContactWebsite))
             .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
             .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
             .ForMember(dest => dest.LastUpdatedBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
             .ForMember(dest => dest.LastUpdatedDate, opt => opt.MapFrom(src => src.LastUpdatedDate))
             .ForMember(dest => dest.NumberOfCountiesServed, opt => opt.MapFrom(src => src.NumberOfCountiesServed))
             .ForMember(dest => dest.NumberOfSponsors, opt => opt.MapFrom(src => src.NumberOfSponsors))
             .ForMember(dest => dest.NumberOfStateStaff, opt => opt.MapFrom(src => src.NumberOfStateStaff))
             .ForMember(dest => dest.NumberOfVolunteerCounselors, opt => opt.MapFrom(src => src.NumberOfVolunteerCounselors))
             .ForMember(dest => dest.ProgramCoordinatorAddress, opt => opt.MapFrom(src => src.ProgramCoordinatorAddress))
             .ForMember(dest => dest.ProgramCoordinatorCity, opt => opt.MapFrom(src => src.ProgramCoordinatorCity))
             .ForMember(dest => dest.ProgramCoordinatorEmail, opt => opt.MapFrom(src => src.ProgramCoordinatorEmail))
             .ForMember(dest => dest.ProgramCoordinatorFax, opt => opt.MapFrom(src => src.ProgramCoordinatorFax))
                //.ForMember(dest => dest.ProgramCoordinatorhone, opt => opt.MapFrom(src => src.ProgramCoordinatorhone))
             .ForMember(dest => dest.ProgramCoordinatorName, opt => opt.MapFrom(src => src.ProgramCoordinatorName))
             .ForMember(dest => dest.ProgramCoordinatorZipcode, opt => opt.MapFrom(src => src.ProgramCoordinatorZipcode))
             .ForMember(dest => dest.ProgramName, opt => opt.MapFrom(src => src.ProgramName))
             .ForMember(dest => dest.ProgramSummary, opt => opt.MapFrom(src => src.ProgramSummary))
             .ForMember(dest => dest.ProgramWebsite, opt => opt.MapFrom(src => src.ProgramWebsite))
             .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.StateName))
             .ForMember(dest => dest.TotalCounties, opt => opt.MapFrom(src => src.TotalCounties))
                //New fields: added by Lavanya Maram - 07/23/2013
              .ForMember(dest => dest.StateOversightAgency, opt => opt.MapFrom(src => src.StateOversightAgency))
              .ForMember(dest => dest.NumberOfPaidStaff, opt => opt.MapFrom(src => src.NumberOfPaidStaff))
              .ForMember(dest => dest.NumberOfCoordinators, opt => opt.MapFrom(src => src.NumberOfCoordinators))
              .ForMember(dest => dest.NumberOfCertifiedCounselors, opt => opt.MapFrom(src => src.NumberOfCertifiedCounselors))
              .ForMember(dest => dest.NumberOfEligibleBeneficiaries, opt => opt.MapFrom(src => src.NumberOfEligibleBeneficiaries))
              .ForMember(dest => dest.NumberOfBeneficiaryContacts, opt => opt.MapFrom(src => src.NumberOfBeneficiaryContacts))
              .ForMember(dest => dest.LocalAgencies, opt => opt.MapFrom(src => src.LocalAgencies))
              .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
              .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude)); 

           }


           public void UpdateShipProfile(EditShipProfileViewData viewData)
           {
               if (viewData == null)
                   throw new ArgumentNullException("viewData");

               new SHIPProfileDAL().UpdateShipProfile(Mapper.Map<EditShipProfileViewData, SHIPProfile>(viewData));
           }


           public DataSet GetSHIPProfileAgencyDetails(string StateFIPS)
           {
               return new SHIPProfileDAL().GetSHIPProfileAgencyDetails(StateFIPS);
           }

           public DataSet GetSHIPProfileAgencyDetailsByAddress(Double Latitude, Double Longitude, string state, int Radius)
           {
               return new SHIPProfileDAL().GetSHIPProfileAgencyDetailsByAddress(Latitude, Longitude, state, Radius);
           }

          

    }
}
