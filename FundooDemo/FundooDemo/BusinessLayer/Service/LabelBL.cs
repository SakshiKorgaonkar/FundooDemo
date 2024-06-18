using BusinessLayer.Interface;
using ModelLayer;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRl;

        public LabelBL(ILabelRL labelRl)
        {
            this.labelRl = labelRl;
        }
        public LabelEntity AddLabel(LabelML label)
        {
            try
            {
                return labelRl.AddLabel(label);
            }
            catch(Exception ex) 
            {
                throw;
            }
        }

        public LabelEntity GetLabel(int id)
        {
            try
            {
                return labelRl.GetLabel(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<LabelEntity> GetLabels()
        {
            try
            {
                return labelRl.GetLabels();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public LabelEntity RemoveLabel(int id)
        {
            try
            {
                return labelRl.RemoveLabel(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public LabelEntity UpdateLabel(int id, LabelML labelMI)
        {
            try
            {
                return labelRl.UpdateLabel(id,labelMI);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
