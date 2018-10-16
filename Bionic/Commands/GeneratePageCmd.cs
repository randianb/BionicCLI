using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using BionicCore.Project;
using BionicPlugin;
using McMaster.Extensions.CommandLineUtils;
using static BionicCore.DirectoryUtils;

namespace BionicCLI.Commands {
  [Command(Description = "Generate Blazor page")]
  public class GeneratePageCmd : CommandBase, ICommand {
    [Argument(0, Description = "Artifact Name"), Required]
    private string Artifact { get; set; }

    public GenerateCommand Parent { get; }

    public GeneratePageCmd() {}

    public GeneratePageCmd(string artifact) => Artifact = artifact;

    protected override int OnExecute(CommandLineApplication app) => GeneratePage();

    public int Execute() => GeneratePage();

    private int GeneratePage() {
      Logger.Success($"Generating a page named {Artifact}");
      Process.Start(
        DotNetExe.FullPathOrDefault(),
        $"new bionic.page -n {Artifact} -p /{GenerateCommand.ToPageName(Artifact)} -o " + ToOSPath("./Pages")
      )?.WaitForExit();
      return GenerateCommand.IntroduceAppCssImport("Pages", Artifact);
    }
  }
}