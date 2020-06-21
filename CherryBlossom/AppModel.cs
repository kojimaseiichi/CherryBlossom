using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CherryBlossom
{
    public class AppModel
    {
        public ObservableCollection<ItemModel> Items { get; private set; } = new ObservableCollection<ItemModel>();
        public ObservableCollection<MorphModel> Morphs { get; private set; } = new ObservableCollection<MorphModel>();
        public ObservableCollection<MessageModel> Messages { get; private set; } = new ObservableCollection<MessageModel>();

        public bool AutoEditMorph { get; set; } = false;

        public AppModel()
        {
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Regex regWS = new Regex(" +");
            if (e.NewItems != null)
            {
                foreach (var o in e.NewItems)
                {
                    ItemModel m = o as ItemModel;
                    var exp = regWS.Split(m.ItemExpression);
                    List<string> unlisted = new List<string>();
                    var morphList = exp.Select(x =>
                    {
                        var one = Morphs.FirstOrDefault(p => p.Morph == x);
                        if (one == null) unlisted.Add(x);
                        return one;
                    });
                }
            }
        }

        async public void Load(string fileName)
        {
            Persistent.SakuraContext context = new Persistent.SakuraContext(fileName);
            context.Database.EnsureCreated();
            var items = await context.Items.ToListAsync();
            var morphs = await context.Morphs.ToListAsync();
            var messages = await context.Messages.ToListAsync();

            Items.Clear();
            Morphs.Clear();
            Messages.Clear();

            morphs.ForEach(x => {
                var model = new MorphModel();
                Migrate(x, model);
                Morphs.Add(model);
            });


            items.ForEach(x => {
                var model = new ItemModel();
                Migrate(x, model);
                Items.Add(model);
            });

            messages.ForEach(x => {
                var model = new MessageModel();
                Migrate(x, model);
                Messages.Add(model);
            });

        }

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

            context.Morphs.AddRange(Morphs.Select(x => {
                Persistent.MorphModel model = new Persistent.MorphModel();
                Migrate(x, model);
                return model;
            }));

            context.Messages.AddRange(Messages.Select(x => {
                Persistent.MessageModel model = new Persistent.MessageModel();
                Migrate(x, model);
                return model;
            }));

            context.SaveChanges();
        }

        private void Migrate<Src, Dest>(Src src, Dest dest)
        {
            foreach (var prop in typeof(Dest).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.Name.StartsWith("_"))
                    continue;
                var srcProp = typeof(Src).GetProperty(prop.Name, BindingFlags.Public | BindingFlags.Instance);
                if (srcProp != null)
                    prop.SetValue(dest, srcProp.GetValue(src));
            }
        }
    }
}
