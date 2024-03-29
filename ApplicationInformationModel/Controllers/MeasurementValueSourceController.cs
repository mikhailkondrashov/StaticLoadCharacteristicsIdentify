﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using ApplicationInformationModel.Model;

namespace ApplicationInformationModel.Controllers
{
    /// <summary>
    /// Controller of class MeasurementValueSource
    /// </summary>
    public class MeasurementValueSourceController
    {
        /// <summary>
        /// Current object of class MeasurementValueSource
        /// </summary>
        public MeasurementValueSource CurrentMeasurementValueSource { get; }

        /// <summary>
        /// Defaults constructor
        /// </summary>
        public MeasurementValueSourceController() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="measurementValueSource"></param>
        public MeasurementValueSourceController(MeasurementValueSource measurementValueSource)
        {
            CurrentMeasurementValueSource = measurementValueSource;
        }

        /// <summary>
        /// Create new object of class MeasurementValueSource
        /// </summary>
        /// <param name="mRid">A Model Authority issues mRIDs</param>
        /// <param name="name">The name is a free text human readable name of the object</param>
        public MeasurementValueSourceController(Guid mRid, string name)
        {
            CurrentMeasurementValueSource = GetMeasurementValueSources().FirstOrDefault(vs => vs.Name == name) ??
                                            new MeasurementValueSource(mRid, name);

            using (var db = new ApplicationsContext())
            {
                db.MeasurementValueSources.AddOrUpdate(CurrentMeasurementValueSource);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Getting list of objects 0f class MeasurementValueSource
        /// </summary>
        /// <returns></returns>
        public List<MeasurementValueSource> GetMeasurementValueSources()
        {
            using (var db = new ApplicationsContext())
            {
                return db.MeasurementValueSources.Where(ms => true).ToList();
            }
        }

        /// <summary>
        /// Getting list of involve objects of class AnalogValue
        /// </summary>
        /// <returns>list of involve objects of class AnalogValue</returns>
        public List<AnalogValue> GetInvolveAnalogValues()
        {
            using (var db = new ApplicationsContext())
            {
                return db.AnalogValues.Where(a => a.MeasurementValueSource_MRID == CurrentMeasurementValueSource.MRID)
                    .ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ObjectHasReferenceException"/>
        public void Delete()
        {
            using (var db = new ApplicationsContext())
            {
                db.MeasurementValueSources.Attach(CurrentMeasurementValueSource);
                db.MeasurementValueSources.Remove(CurrentMeasurementValueSource);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    throw new ObjectHasReferenceException($"{this}. The DELETE statement conflicted with the REFERENCE constraint");
                }
            }
        }
    }
}