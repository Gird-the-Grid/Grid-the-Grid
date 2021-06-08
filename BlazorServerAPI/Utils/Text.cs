using System;

namespace BlazorServerAPI.Utils
{
    public static class Text
    {
        public static string Unknown => "Unknown";
        public static string Secret => "SECRET";
        public static string Id => "id";
        public static string InvalidCredentials => "Invalid credentials";
        public static string InvalidConfirmationLink => "Invalid confirmation link. Link may have expired.";
        public static string UserCreated => "User created. Confirmation Mail Sent.";
        public static string UserConfirmed => "User confirmed.";
        public static string InvalidUserId => "Invalid user id";
        public static string ResetMailSent => "Reset Password Mail Sent.";
        public static string PasswordReset => "Password has been reset.";
        public static string NotEnoughEntropy => "PasswordHasher failed, not enough entropy";
        public static string Forbidden => "Forbidden";
        public static string ServerException => "Server exception";
        public static string InternalServerError => "Internal server error";
        public static string EmailUsed => "Email used";
        public static string Company => "Company";
        public static string Grid => "Grid";
        public static string Deleted => "Deleted";
        public static string UnconfiguredGrid => "Grid has no template";
        public static string UnconfiguredCompany => "Company has no configuration";
        public static string UserId => "UserId";
        public static string Authorization => "Authorization";
        public static string Space => " ";
        public static string InvalidToken => "Invalid token";
        public static string CompanyConfigurationError => "Cannot have more than 1 company configuration";
        public static string GridConfigurationError => "Cannot have more than 1 grid template";
        public static string CompanyConfigurationExists => "This company's configuration already exists";
        public static string UnownedResource => "User doesn't own this resource";
        public static string ResourceDoesNotExist => "Cannot update non-existent resource";
        public static string IllegalModification => "Invalid company id or illegal company modification";
        public static string BaseUrl => "http://localhost:49481/";
        public static string MongoDbUri => "MONGODB_URI";
        public static string MongoDbDb => "MONGODB_DB";
        public static string MongoDbId => "_id";

        public static string InvalidConfirmationString(string userId)
        {
            return $"{userId} is not a valid confirmation link";
        }

        public static string SettingsUpdated(Type ownedEntity)
        {
            return $"{ownedEntity} settings updated";
        }
        
        public static string NoConfiguration(Type type)
        {
            return $"{type} has no configuration";
        }

        public static string Exception(Exception e)
        {
            return $"Exception: {e}, {e.Message}";
        }

        public static string AuthConfirmationUrl(string userId)
        {
            return $"http://localhost:49429/auth/confirm?userId={userId}";
        }

        
    }
}
