using System;
using System.IO;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;

namespace FluentMigrator.Tests.Unit.Runners
{
	public class MigratorConsole
	{
		public string ProcessorType;
		public IMigrationProcessor Processor;
		public string Connection;
		public bool Log;
		private string TargetAssembly;
	    public string Namespace;

	    public MigratorConsole(string[] args)
		{
			ParseArguments(args);
			CreateProcessor();
			ExecuteMigrations();
		}

		private void ParseArguments(string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].Contains("/db"))
					ProcessorType = args[i + 1];

				if (args[i].Contains("/connection"))
					Connection = args[i + 1];

				if (args[i].Contains("/target"))
					TargetAssembly = args[i + 1];

				if (args[i].Contains("/log"))
					Log = true;

                if (args[i].Contains("/namespace"))
                    Namespace = args[i + 1];
			}

			if (string.IsNullOrEmpty(ProcessorType))
				throw new ArgumentException("Database Type is required (/database)");
			if (string.IsNullOrEmpty(Connection))
				throw new ArgumentException("Connection String is required (/connection");
		}

		private void CreateProcessor()
		{
			IMigrationProcessorFactory processorFactory = ProcessorFactory.GetFactory(ProcessorType);
			Processor = processorFactory.Create(Connection);
		}

		private void ExecuteMigrations()
		{
			if (!Path.IsPathRooted(TargetAssembly))
				TargetAssembly = Path.GetFullPath(TargetAssembly);

			Assembly assembly = Assembly.LoadFile(TargetAssembly);
			var runner = new MigrationVersionRunner(new MigrationConventions(), Processor, new MigrationLoader(new MigrationConventions()), assembly, Namespace);
			runner.LoadAssemblyMigrations();
			runner.UpgradeToLatest(true);
		}
	}
}