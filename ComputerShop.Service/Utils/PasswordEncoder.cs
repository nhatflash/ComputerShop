using System;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace ComputerShop.Service.Utils;

public class PasswordEncoder
{

    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 4;
    private const int MemorySize = 65536; 
    private const int DegreeOfParallelism = 4;


    public string HashPassword(string rawPassword)
    {
        byte[] salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt);

        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(rawPassword))
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            Iterations = Iterations,
            MemorySize = MemorySize,
        };

        byte[] hash = argon2.GetBytes(HashSize);

        byte[] combined = new byte[SaltSize + HashSize];
        Buffer.BlockCopy(salt, 0, combined, 0, SaltSize);
        Buffer.BlockCopy(hash, 0, combined, SaltSize, HashSize);

        return Convert.ToBase64String(combined);
    }


    public bool VerifyPassword(string rawPassword, string hashedPassword)
    {
        byte[] combined = Convert.FromBase64String(hashedPassword);

        byte[] salt = new byte[SaltSize];
        byte[] hash = new byte[HashSize];

        Buffer.BlockCopy(combined, 0, salt, 0, SaltSize);
        Buffer.BlockCopy(combined, SaltSize, hash, 0, HashSize);

        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(rawPassword))
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            Iterations = Iterations,
            MemorySize = MemorySize,
        };

        byte[] newHash = argon2.GetBytes(HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, newHash);
    }
}
