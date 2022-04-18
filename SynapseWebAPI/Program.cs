namespace SynapseWebAPI
{
    public static class Program
    {
        private const string ConfigurationFolder = "";
        private static readonly string[] ConfigurationFiles = { "appsettings.json" };

        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Initialize()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            var configurationFiles = ConfigurationFiles;
                            if (hostingContext.HostingEnvironment.IsDevelopment())
                            {
                                configurationFiles = System.Environment.GetEnvironmentVariable("CONFIG_FILE")?.Split(";");
                                if (configurationFiles == null || configurationFiles.Length == 0)
                                {
                                    return;
                                }
                            }

                            foreach (string file in configurationFiles)
                            {
                                string basePath = Path.Combine(Directory.GetCurrentDirectory(), ConfigurationFolder);
                                string configFile = Path.Combine(basePath, file);
                                if (File.Exists(configFile))
                                {
                                    config.SetBasePath(basePath);
                                    // NOTE: if we add XML files here, we will need to look at the extension and branch accordingly.
                                    config.AddJsonFile(configFile);
                                }
                                else
                                {
                                    throw new InvalidOperationException($"Unable to find configuration file: {configFile}");
                                }
                            }
                        })
                        .ConfigureKestrel((context, serverOptions) =>
                        {
                        });
                });
        /// <summary>
        /// Custom initialization that must happen before requests are accepted
        /// </summary>
        /// <param name="webHost">Web host</param>
        /// <returns>Web host after processing initialization</returns>
        internal static IHost Initialize(this IHost webHost)
        {
            return webHost;
        }
    }
}
