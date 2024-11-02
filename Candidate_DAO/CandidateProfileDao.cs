using Candidate_BusinessObject;
using Candidate_BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_DAO
{
    public class CandidateProfileDao
    {
        private static CandidateProfileDao instance;

        public CandidateManagementContext dbContext;

        public CandidateProfileDao()
        {
            dbContext = new CandidateManagementContext();
        }

        public static CandidateProfileDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CandidateProfileDao();
                }
                return instance;
            }
        }

        public List<CandidateProfile> GetCandidateProfiles()
        {
            return dbContext.CandidateProfiles.ToList();
        }

        public CandidateProfile GetCandidateProfile(string id) 
        {
            return dbContext.CandidateProfiles.SingleOrDefault(x => x.CandidateId.Equals(id));
        }

        public bool AddProfile(CandidateProfile candidateProfile)
        {
            bool isSuccess = false;
            CandidateProfile candidate = GetCandidateProfile(candidateProfile.CandidateId) ;
            try
            {
                if (candidate == null)
                {
                    dbContext.CandidateProfiles.Add(candidateProfile);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save");
            }
            return isSuccess;
        }

        public bool DeleteProfile(CandidateProfile candidateProfile)
        {
            bool isSuccess = false;
            CandidateProfile candidate = GetCandidateProfile(candidateProfile.CandidateId);
            try
            {
                if (candidate != null)
                {
                    dbContext.CandidateProfiles.Remove(candidate);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not delete");
            }
            return isSuccess;
        }

        public bool UpdateProfile(CandidateProfile candidateProfile)
        {
            bool isSuccess = false;

            try
            {
                // Fetch the existing candidate profile from the database
                CandidateProfile existingCandidate = GetCandidateProfile(candidateProfile.CandidateId);

                if (existingCandidate != null)
                {
                    // Update the properties of the existing candidate profile
                    existingCandidate.Fullname = candidateProfile.Fullname;
                    existingCandidate.Birthday = candidateProfile.Birthday;
                    existingCandidate.ProfileShortDescription = candidateProfile.ProfileShortDescription;
                    existingCandidate.ProfileUrl = candidateProfile.ProfileUrl;
                    existingCandidate.PostingId = candidateProfile.PostingId;

                    // Mark the existing entity as modified
                    dbContext.Entry(existingCandidate).State = EntityState.Modified;

                    // Save changes to the database
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (use your preferred logging framework)
                // For example: _logger.LogError(ex, "Error updating profile for candidate ID: {CandidateId}", candidateProfile.CandidateId);
                // You might also want to handle specific exception types here
            }

            return isSuccess;
        }
    }
}
