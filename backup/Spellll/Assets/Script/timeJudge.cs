using System.Collections;
using UnityEngine;
using System;

public static class TimeJuede
{
    private static long GetTimestamp()
    {
        TimeSpan ts = System.DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);//ToUniversalTime()转换为标准时区的时间,去掉的话直接就用北京时间
        return (long)ts.TotalMilliseconds; //精确到毫秒
                                           //return (long)ts.TotalSeconds;//获取10位
    }

    public static bool OverTime(ref long old_time,long Milliseconds)
    {
        long new_time = GetTimestamp();

        if (new_time - old_time > Milliseconds)
        {
            old_time = new_time;
            return true;
        }
        return false;

        //return new_time - old_record > Milliseconds ? true : false;
    }
}
