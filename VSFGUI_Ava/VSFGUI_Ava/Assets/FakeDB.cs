using System.Collections.Generic;
using System.Net;
using Rug.Osc;
using VSFGUI_Ava.Models;

namespace VSFGUI_Ava.Assets;

public class FakeDB
{
    public IEnumerable<OscInput> GetMessages() => new[]
    {
        new OscInput() { Channel = "/TOsc/Test", ID = "TwitchMessage" },
        new OscInput() { Channel = "/TOsc/Test", ID = "Brugh" },
        new OscInput() { Channel = "/TOsc/Test", ID = "Bingus" },
    };
}