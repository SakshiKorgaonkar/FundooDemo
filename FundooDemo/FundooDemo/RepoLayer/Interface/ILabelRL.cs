using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity AddLabel(LabelML label);
        public LabelEntity RemoveLabel(int id);
        public List<LabelEntity> GetLabels();
        public LabelEntity GetLabel(int id);
        public LabelEntity UpdateLabel(int id,LabelML labelMI);
    }
}
