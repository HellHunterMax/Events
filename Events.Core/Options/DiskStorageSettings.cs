namespace Events.Core.Options
{
    public class DiskStorageSettings
    {
        public const string DiskStorage = nameof(DiskStorageSettings);

        public string Location { get; set; } = String.Empty;
    }
}
