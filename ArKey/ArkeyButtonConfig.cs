using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArKey
{
    internal class ArkeyButtonConfig
    {
        public int ButtonID { get; set; }
        public string ButtonAction { get; set; }
        public bool ButtonSingle { get; set; }
        public bool TypeMode { get; set; }
        public bool Pressed { get; internal set; }
        public bool Multikey { get; set; }

        /// <summary>
        /// ArKey Class, Setup Buttons
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Action"></param>
        /// <param name="Single"></param>
        /// <param name="typeMode"></param>
        public ArkeyButtonConfig(int id, string Action, bool Single, bool typeMode, bool MultiKey)
        {
            ButtonID = id; ButtonAction = Action; ButtonSingle = Single; TypeMode = typeMode; Multikey = MultiKey; 
        }

        public ArkeyButtonConfig(int id, string Action)
        {
            ButtonID = id;
            ButtonAction = Action;
            ButtonSingle = true;
            TypeMode = false;
        }
    }
}
