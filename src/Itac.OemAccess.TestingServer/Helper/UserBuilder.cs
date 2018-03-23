using System.Collections.Generic;
using Itac.OemAccess.TestingServer.Model;

namespace Itac.OemAccess.TestingServer.Helper
{
    
    public class UserBuilder
    {
        public static UserEntity NewUser(

            string description = "Default User",
            string userId = "User-1",
            string userType = "DefaultUserType",
            List<string> attributes = null,
            List<UserEntity.UserIdentifier> identifier = null,
            List<UserEntity.UserVerifier> verifier = null,
            Dictionary<string, List<UserEntity.UserPermission>> permission = null
            )
        {

            return new UserEntity()
            {
                Description = description,
                Attributes = GetAttributes(attributes),
                Id = userId,
                Identifiers = GetIdentifiers(identifier),
                Permissions = GetPermissions(permission),
                Type = userType,
                Verifiers = GetVerifiers(verifier)
            };
        }
        private static List<string> GetAttributes(List<string> attributes)
        {
            if (attributes == null)
                attributes = new List<string>() { "Blind", "Clumsy" };
            return attributes;
        }

        private static List<UserEntity.UserVerifier> GetVerifiers(List<UserEntity.UserVerifier> verifier)
        {
            if (verifier == null)
                return new List<UserEntity.UserVerifier>()
            {
                new UserEntity.UserVerifier()
                {
                    Data = "UserVerifierData",
                    Description = "UserVerifierDescription",
                    Duress = false,
                    Id = "UserVerifierID",
                    Type = "UserVerifierType"
                },
                new UserEntity.UserVerifier()
                {
                    Data = "UserVerifierData1",
                    Description = "UserVerifierDescription1",
                    Duress = true,
                    Id = "UserVerifierID1",
                    Type = "UserVerifierType1"
                }
            };
            return verifier;
        }

        private static Dictionary<string, List<UserEntity.UserPermission>> GetPermissions(Dictionary<string, List<UserEntity.UserPermission>> permission)
        {
            if (permission == null)
            {
                var permissionDic = new Dictionary<string, List<UserEntity.UserPermission>>();
                var permissionList = new List<UserEntity.UserPermission>
                {
                    new UserEntity.UserPermission()
                    {
                        Data = "UserPermissionData",
                        Type = "UserPermissionType"
                    }
                };
                permissionDic.Add("Permission", permissionList);

                return permissionDic;
            }
            return permission;
        }

        private static List<UserEntity.UserIdentifier> GetIdentifiers(List<UserEntity.UserIdentifier> identifier)
        {
            if (identifier == null)
            {
                identifier = new List<UserEntity.UserIdentifier>()
                {
                    new UserEntity.UserIdentifier()
                    {
                        Data = "UserIdentifierData",
                        Description = "UserIdentifierDescription",
                        Id = "UserIdentifierId",
                        Type = "UserIdentifierType"
                    },
                    new UserEntity.UserIdentifier()
                    {
                        Data = "UserIdentifierData1",
                        Description = "UserIdentifierDescription1",
                        Id = "UserIdentifierId1",
                        Type = "UserIdentifierType1"
                    }
                };
            }
            return identifier;
        }
    }
}
