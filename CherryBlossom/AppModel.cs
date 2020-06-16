using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.Linq;

namespace CherryBlossom
{
    public class AppModel
    {
        public ObservableCollection<ItemModel> Items { get; set; } = new ObservableCollection<ItemModel>();
        public ObservableCollection<MorphModel> Morphs { get; set; } = new ObservableCollection<MorphModel>();

        public void Save(string fileName)
        {
            Persistent.SakuraContext context = new Persistent.SakuraContext(fileName);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Items.AddRange(Items.Select(x => {
                Persistent.ItemModel model = new Persistent.ItemModel();
                Migrate(x, model);
                return model;
            }));
        }

        private void Migrate<Src, Dest>(Src src, Dest dest)
        {
            foreach (var prop in typeof(Dest).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.Name.StartsWith("_"))
                    continue;
                var srcProp = typeof(Src).GetProperty(prop.Name, BindingFlags.Public | BindingFlags.Instance);
                prop.SetValue(dest, srcProp.GetValue(src));
            }
        }
    }
}
