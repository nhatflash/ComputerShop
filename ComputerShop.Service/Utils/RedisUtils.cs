using System;
using ComputerShop.Repository.CustomExceptions;
using Microsoft.Extensions.Caching.Distributed;

namespace ComputerShop.Service.Utils;

public class RedisUtils
{
    private readonly IDistributedCache _redisCache;

    public RedisUtils(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task StoreStringData(string key, string data, TimeSpan expiration)
    {
        await _redisCache.SetStringAsync(key, data, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        });
    }

    public async Task StoreLongData(string key, long data, TimeSpan expiration)
    {
        string dataStr = data.ToString();
        await StoreStringData(key, dataStr, expiration);
    }


    public async Task StoreGuidData(string key, Guid data, TimeSpan expiration)
    {
        string dataStr = data.ToString();
        await StoreStringData(key, dataStr, expiration);
    }


    public async Task<string?> GetStringDataFromKey(string key)
    {
        return await _redisCache.GetStringAsync(key);
    }


    public async Task<long> GetLongDataFromKey(string key)
    {
        string data = await _redisCache.GetStringAsync(key) ?? throw new NotFoundException($"No key found or expired: {key}");
        bool success = long.TryParse(data, out long result);
        if (success)
        {
            return result;
        }
        throw new BadRequestException($"Invalid format from the value that stored in key: {key}");
    }

    public async Task<Guid> GetGuidDataFromKey(string key)
    {
        string data = await _redisCache.GetStringAsync(key) ?? throw new NotFoundException($"No key found or expired: {key}");
        bool success = Guid.TryParse(data, out Guid result);
        if (success)
        {
            return result;
        }
        throw new BadRequestException($"Invalid format from the value that stored in key: {key}");
    }
}
