using ModelLayer;
using RepoLayer.Context;
using RepoLayer.CustomException;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class LabelRI : ILabelRI
    {
        private readonly ProjectContext projectContext;

        public LabelRI(ProjectContext projectContext)
        {
            this.projectContext = projectContext;
        }
        public Label AddLabel(LabelMI label) 
        {
            Label label1 = new Label();
            label1.Name = label.Name;
            projectContext.Labels.Add(label1);
            projectContext.SaveChanges();
            return label1;
        }
        public Label RemoveLabel(int id) 
        {
            var label = projectContext.Labels.FirstOrDefault(x => x.Id == id);
            if (label == null) 
            {
                throw new CustomException1("Label with given id doesn't exist");
            }
            projectContext.Labels.Remove(label);
            projectContext.SaveChanges();
            return label;
        }
        public Label GetLabel(int id)
        {
            var label = projectContext.Labels.FirstOrDefault(x => x.Id == id);
            if (label == null)
            {
                throw new CustomException1("Label with given id doesn't exist");
            }
            return label;
        }
        public List<Label> GetLabels()
        {
            if(projectContext.Labels == null)
            {
                throw new CustomException1("No labels added");
            }
            return projectContext.Labels.ToList();
        }
        public Label UpdateLabel(int id, LabelMI labelMI)
        {
            var label = projectContext.Labels.FirstOrDefault(x => x.Id == id);
            if (label == null)
            {
                throw new CustomException1("Label with given id doesn't exist");
            }
            label.Name = labelMI.Name;
            projectContext.Labels.Update(label);
            projectContext.SaveChanges();
            return label;
        }
    }
}
