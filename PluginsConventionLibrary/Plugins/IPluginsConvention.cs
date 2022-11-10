namespace PluginsConventionLibrary.Plugins
{
    public interface IPluginsConvention
    {
        string PluginName { get; }
        UserControl GetControl { get; }

        PluginsConventionElement GetElement { get; }

        Form GetForm(PluginsConventionElement element);

        bool DeleteElement(PluginsConventionElement element);

        void ReloadData();

        bool CreateSimpleDocument(PluginsConventionSaveDocument saveDocument);

        bool CreateTableDocument(PluginsConventionSaveDocument saveDocument);

        bool CreateChartDocument(PluginsConventionSaveDocument saveDocument);
    }
}
