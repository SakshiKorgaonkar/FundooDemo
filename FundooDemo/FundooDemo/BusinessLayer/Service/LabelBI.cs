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
    public class LabelBI : ILabelBI
    {
        private readonly ILabelRI labelRI;

        public LabelBI(ILabelRI labelRI)
        {
            this.labelRI = labelRI;
        }
        public Label AddLabel(LabelMI label)
        {
            try
            {
                return labelRI.AddLabel(label);
            }
            catch(Exception ex) 
            {
                throw;
            }
        }

        public Label GetLabel(int id)
        {
            try
            {
                return labelRI.GetLabel(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Label> GetLabels()
        {
            try
            {
                return labelRI.GetLabels();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Label RemoveLabel(int id)
        {
            try
            {
                return labelRI.RemoveLabel(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Label UpdateLabel(int id, LabelMI labelMI)
        {
            try
            {
                return labelRI.UpdateLabel(id,labelMI);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
