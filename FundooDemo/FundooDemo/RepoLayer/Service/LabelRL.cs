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
    public class LabelRL : ILabelRL
    {
        private readonly ProjectContext projectContext;

        public LabelRL(ProjectContext projectContext)
        {
            this.projectContext = projectContext;
        }
        public LabelEntity AddLabel(LabelML label) 
        {
            LabelEntity label1 = new LabelEntity();
            label1.Name = label.Name;
            projectContext.Labels.Add(label1);
            projectContext.SaveChanges();
            return label1;
        }
        public LabelEntity RemoveLabel(int id) 
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
        public LabelEntity GetLabel(int id)
        {
            var label = projectContext.Labels.FirstOrDefault(x => x.Id == id);
            if (label == null)
            {
                throw new CustomException1("Label with given id doesn't exist");
            }
            return label;
        }
        public List<LabelEntity> GetLabels()
        {
            if(projectContext.Labels == null)
            {
                throw new CustomException1("No labels added");
            }
            return projectContext.Labels.ToList();
        }
        public LabelEntity UpdateLabel(int id, LabelML labelMI)
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
