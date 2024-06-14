using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface ILabelRI
    {
        public Label AddLabel(LabelMI label);
        public Label RemoveLabel(int id);
        public Label GetLabel(int id);
        public Label UpdateLabel(int id,LabelMI labelMI);

    }
}
