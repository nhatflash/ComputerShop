using System;

namespace ComputerShop.Service.Constant;

public class RedisKey
{

    public static string GetPayOSOrderKey(long orderCode)
    {
        return $"payos_order_{orderCode}:";
    }
}
