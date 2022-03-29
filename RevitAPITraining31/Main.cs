using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining31
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var selectedRef = uidoc.Selection.PickObject(ObjectType.Face, "Веберите стену по грани");
            var selectedElement = doc.GetElement(selectedRef);

            if (selectedElement is Wall)
            {
                //Parameter lengthParameter1 = selectedElement.LookupParameter("Объем");
                //if (lengthParameter1.StorageType == StorageType.Double)
                //{
                //    TaskDialog.Show("Объем1", lengthParameter1.AsDouble().ToString());
                //}

                Parameter volumeParameter2 = selectedElement.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);
                if (volumeParameter2.StorageType == StorageType.Double)
                {
                    double volumeValue = UnitUtils.ConvertFromInternalUnits(volumeParameter2.AsDouble(), UnitTypeId.CubicMeters);
                    TaskDialog.Show("Объем стены", volumeValue.ToString());
                    //TaskDialog.Show("Объем2", lengthParameter2.AsDouble().ToString());
                }
            }


            return Result.Succeeded;
        }
    }

}
