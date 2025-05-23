﻿using DownKyi.Core.BiliApi.Video.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using Console = DownKyi.Core.Utils.Debugging.Console;

namespace DownKyi.Core.BiliApi.Video;

public static class Dynamic
{
    /// <summary>
    /// 获取分区最新视频列表
    /// </summary>
    /// <param name="rid">目标分区tid</param>
    /// <param name="pn">页码</param>
    /// <param name="ps">每页项数（最大50）</param>
    /// <returns></returns>
    public static List<DynamicVideoView>? RegionDynamicList(int rid, int pn = 1, int ps = 5)
    {
        var url = $"https://api.bilibili.com/x/web-interface/dynamic/region?rid={rid}&pn={pn}&ps={ps}";
        const string referer = "https://www.bilibili.com";
        var response = WebClient.RequestWeb(url, referer);

        try
        {
            var dynamic = JsonConvert.DeserializeObject<RegionDynamicOrigin>(response);
            if (dynamic != null && dynamic.Data != null)
            {
                return dynamic.Data.Archives;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.PrintLine("RegionDynamicList()发生异常: {0}", e);
            LogManager.Error("Dynamic", e);
            return null;
        }
    }
}