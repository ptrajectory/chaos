

namespace chaos.utils;

public static class AppEnvironment {

    public static string? FRIEND_API_KEY {
        get {
            return Environment.GetEnvironmentVariable("FRIEND_API_KEY");
        }
        private set{

        }
    }

    public static byte[] APP_RSA_KEY_PAIR {
        get {
            return File.ReadAllBytes("./key");
        }
        private set{

        }
    }

    public static string? ENCRYPTION_KEY {
        get {
            return Environment.GetEnvironmentVariable("ENCRYPTION_KEY");
        }
        private set {
            
        }
    }



}