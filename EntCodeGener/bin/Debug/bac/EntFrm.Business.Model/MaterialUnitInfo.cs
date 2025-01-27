using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Business.Model
{
  public class MaterialUnitInfo
  {
     public MaterialUnitInfo(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string MatUntNo;
       public string sMatUntNo
       {
           set { this.MatUntNo =value;}
           get { return this.MatUntNo;}
        }

       private string MatUntName;
       public string sMatUntName
       {
           set { this.MatUntName =value;}
           get { return this.MatUntName;}
        }

       private int IsDefaultUnit;
       public int iIsDefaultUnit
       {
           set { this.IsDefaultUnit =value;}
           get { return this.IsDefaultUnit;}
        }

       private string UnitDeep;
       public string sUnitDeep
       {
           set { this.UnitDeep =value;}
           get { return this.UnitDeep;}
        }

       private string DefaultUnit;
       public string sDefaultUnit
       {
           set { this.DefaultUnit =value;}
           get { return this.DefaultUnit;}
        }

       private string AddOptor;
       public string sAddOptor
       {
           set { this.AddOptor =value;}
           get { return this.AddOptor;}
        }

       private DateTime AddDate;
       public DateTime dAddDate
       {
           set { this.AddDate =value;}
           get { return this.AddDate;}
        }

       private string ModOptor;
       public string sModOptor
       {
           set { this.ModOptor =value;}
           get { return this.ModOptor;}
        }

       private DateTime ModDate;
       public DateTime dModDate
       {
           set { this.ModDate =value;}
           get { return this.ModDate;}
        }

       private int ValidityState;
       public int iValidityState
       {
           set { this.ValidityState =value;}
           get { return this.ValidityState;}
        }

       private string Comments;
       public string sComments
       {
           set { this.Comments =value;}
           get { return this.Comments;}
        }

       private string AppCode;
       public string sAppCode
       {
           set { this.AppCode =value;}
           get { return this.AppCode;}
        }

       private string Version;
       public string sVersion
       {
           set { this.Version =value;}
           get { return this.Version;}
        }

    }
  }

