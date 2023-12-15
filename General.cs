using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfGenerator;

public class General
{
    public const string ProjectPath = "D:\\DotNetProjects6\\PdfGenerator\\";
    public const string PdfInputFile = ProjectPath + "InputFiles\\AirSample1.pdf";
    public const string PdfOutputFile = ProjectPath + "OutputFiles\\TestSample1.pdf";
    public const string PdfOutputQuestTestFile = ProjectPath + "OutputFiles\\QuestTestSample1.pdf";

    public const string Sample_TemplateInputFile = ProjectPath + "InputFiles\\SampleTemplate.pdf";
    public const string Sample_TemplateOutputFile = ProjectPath + "OutputFiles\\SampleTemplateFilled.pdf";
    
    public const string Sample_WatermarkOutputFile = ProjectPath + "OutputFiles\\SampleWatermark.pdf";

    public const string LogoImageFile = ProjectPath + "Images\\air-logo.png";
    public const string Sample_CreatePdfOutpuFile = ProjectPath + "OutputFiles\\sample1.pdf";
    public const string Sample_ClockOutpuFile = ProjectPath + "OutputFiles\\clock.pdf";
    public const string Sample_XFormOutpuFile = ProjectPath + "OutputFiles\\xform_output.pdf";
    public const string Sample_ImageFile = ProjectPath + "Images\\gespraech.jpg";

}
