using EX.Model.DbLayer;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EX.Model.Repositories.Administration
{
    public class TabRepository
    {
        EContext context;

        public TabRepository(int roleId)
        {
            context = new EContext();

            context.Tabs.Add(new Tab { Name = "General", IsChecked = true, IsSelected = true, RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "File", IsChecked = true, IsSelected = false, RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Configuration", IsChecked = true, IsSelected = false, RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Administration", IsChecked = true, IsSelected = false, RoleId = roleId });
            context.Tabs.Add(new Tab { Name = "Net", IsChecked = true, IsSelected = false, RoleId = roleId });
            context.SaveChanges();

            var ConfigurationId = context.Tabs.Where(t => t.Name == "Configuration").Select(s => s.Id).FirstOrDefault();
            context.SubTabs.Add(new SubTab { Name = "LabelFilds", IsChecked = true, TabId = ConfigurationId });
            context.SubTabs.Add(new SubTab { Name = "BageColor", IsChecked = true, TabId = ConfigurationId });
            context.SubTabs.Add(new SubTab { Name = "Desctop", IsChecked = true, TabId = ConfigurationId });
            context.SubTabs.Add(new SubTab { Name = "Raport", IsChecked = true, TabId = ConfigurationId });

            var NetId = context.Tabs.Where(t => t.Name == "Net").Select(s => s.Id).FirstOrDefault();
            context.SubTabs.Add(new SubTab { Name = "Mode", IsChecked = true, TabId = NetId });

            var AdministrationId = context.Tabs.Where(t => t.Name == "Administration").Select(s => s.Id).FirstOrDefault();
            context.SubTabs.Add(new SubTab { Name = "Registration", IsChecked = true, TabId = AdministrationId });
            context.SubTabs.Add(new SubTab { Name = "Authorization", IsChecked = true, TabId = AdministrationId });
            context.SubTabs.Add(new SubTab { Name = "Manage", IsChecked = true, TabId = AdministrationId });
            context.SaveChanges();
        }

        public void RemoveCurrentTabRepository(int roleId)
        {
            var delTabs = context.Tabs.Where(t => t.RoleId == roleId);
            foreach (var t in delTabs)
            {
                var delSubTabs = context.SubTabs.Where(s => s.TabId == t.Id);
                if (delSubTabs != null) foreach (var s in delSubTabs) { context.SubTabs.Remove(s); }
                context.Tabs.Remove(t);
            }
            context.SaveChanges();
        }

        public IEnumerable<Tab> GetAllTabs() { return context.Tabs; }

        public IEnumerable<SubTab> GetSubTubs(Tab tab) { return context.SubTabs.Where(s => s.TabId == tab.Id); }

        public IEnumerable<SubTab> GetAllSubTibs() { return context.SubTabs; }

        public void UpdateTabRepository(IEnumerable<Tab> tabs, IEnumerable<SubTab> subTabs)
        {
            foreach (var t in tabs) { context.Tabs.AddOrUpdate(t); }
            foreach (var s in subTabs) { context.SubTabs.AddOrUpdate(s); }
            context.SaveChanges();
        }

        //public void UpdateTabRepository(Tab tab, IEnumerable<SubTab> subTabs)
        //{
        //    context.Tabs.AddOrUpdate(tab); 
        //    foreach (var s in subTabs) { context.SubTabs.AddOrUpdate(s); }
        //    context.SaveChanges();
        //}
    }
}
