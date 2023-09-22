using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chaos.Dtos.Channel
{
    public class CreateChannel
    {

        public string CreatorID {get; set;} = String.Empty;

        public string Name {get; set;} = String.Empty;

        public string Description {get; set;} = String.Empty;

        public string Icon {get; set;} = String.Empty;

        public string Banner {get; set;} = String.Empty;
    }
}