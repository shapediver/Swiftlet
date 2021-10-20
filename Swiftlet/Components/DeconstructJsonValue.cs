﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Newtonsoft.Json.Linq;
using Rhino.Geometry;
using Swiftlet.Goo;
using Swiftlet.Params;
using Swiftlet.Util;

namespace Swiftlet.Components
{
    public class DeconstructJsonValue : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DeconstructJsonObject class.
        /// </summary>
        public DeconstructJsonValue()
          : base("Deconstruct JSON Value", "DJSON",
              "Deconstruct JSON Value",
              NamingUtility.CATEGORY, NamingUtility.READ)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new JTokenParam(), "JToken", "J", "JSON Value to be deconstructed", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("AsString", "S", "JSON Value as string", GH_ParamAccess.item);
            pManager.AddNumberParameter("AsNumber", "N", "JSON Value as number", GH_ParamAccess.item);
            pManager.AddBooleanParameter("AsBool", "B", "JSON Value as boolean", GH_ParamAccess.item);
            pManager.AddParameter(new JTokenParam(), "AsObject", "O", "JSON Value as JObject", GH_ParamAccess.item);
            pManager.AddParameter(new JArrayParam(), "AsArray", "A", "JSON Value as JArray", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            JTokenGoo goo = null;
            DA.GetData(0, ref goo);


            JToken input_token = goo.Value;

            try { DA.SetData(0, input_token.ToString()); }
            catch { }
            try { DA.SetData(1, input_token.ToObject<double>()); }
            catch { }
            try { DA.SetData(2, input_token.ToObject<bool>()); }
            catch { }

            if (input_token is JObject)
            {
                DA.SetData(3, new JTokenGoo(input_token));
            }
            if (input_token is JArray)
            {
                DA.SetData(4, new JArrayGoo(input_token as JArray));
            }

            
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("d59d2f8a-23d2-4e84-9a80-1ac245ea57e5"); }
        }
    }
}