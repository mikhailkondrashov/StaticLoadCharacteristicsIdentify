﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using ApplicationInformationModel.Model;

namespace ApplicationInformationModel.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class EnergyConsumerController
    {
        /// <summary>
        /// Current object of Class EnergyConsumer
        /// </summary>
        public EnergyConsumer CurrentEnergyConsumer { get; }

        /// <summary>
        /// Defaults constructor 
        /// </summary>
        public EnergyConsumerController() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="energyConsumer"></param>
        public EnergyConsumerController(EnergyConsumer energyConsumer)
        {
            CurrentEnergyConsumer = energyConsumer;
        }

        /// <summary>
        /// Create new object of Class EnergyConsumer
        /// </summary>
        /// <param name="mRid">A Model Authority issues mRIDs</param>
        /// <param name="name">The name is a free text human readable name of the object</param>
        /// <param name="customerCount">Number of individual customers represented by this Demand</param>
        /// <param name="pfixed">Active power of the load that is a fixed quantity</param>
        /// <param name="qfixed">Reactive power of the load that is a fixed quantity</param>
        /// <param name="pfixedPct">Fixed active power as per cent of load group fixed active power</param>
        /// <param name="qfixedPct">Fixed reactive power as per cent of load group fixed reactive power</param>
        public EnergyConsumerController(Guid mRid, string name, int customerCount, double pfixed, double qfixed, double pfixedPct, double qfixedPct)
        {
            using (var db = new ApplicationsContext())
            {
                CurrentEnergyConsumer = db.EnergyConsumers.FirstOrDefault(e=>e.Name==name) ?? new EnergyConsumer(mRid, name, customerCount, pfixed, qfixed, pfixedPct, qfixedPct);
                db.EnergyConsumers.AddOrUpdate(CurrentEnergyConsumer);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<StaticLoadCharacteristic> GetInvolveStaticLoadCharacteristics()
        {
            using (var db = new ApplicationsContext())
            {
                return db.StaticLoadCharacteristics.Where(s=>s.EnergyConsumer_MRID==CurrentEnergyConsumer.MRID).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EnergyConsumer> GetEnergyConsumers()
        {
            using (var db = new ApplicationsContext())
            {
                return db.EnergyConsumers.Where(e => true).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Analog> GetInvolveAnalogs()
        {
            using (var db = new ApplicationsContext())
            {
                return db.Analogs.Where(s => s.EnergyConsumer_MRID == CurrentEnergyConsumer.MRID).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="substation"></param>
        public void SetSubstation(Substation substation)
        {
            CurrentEnergyConsumer.SetSubstation(substation);
            Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Substation GetSubstation()
        {
            using (var db = new ApplicationsContext())
            {
                return db.Substations.FirstOrDefault(s =>
                    s.MRID == db.EnergyConsumers.FirstOrDefault(e => e.MRID == CurrentEnergyConsumer.MRID)
                        .Substation_MRID);
            }
        }

        /// <summary>
        /// Update current object EnergyConsumer
        /// </summary>
        public void Update()
        {
            using (ApplicationsContext db = new ApplicationsContext())
            {
                db.EnergyConsumers.AddOrUpdate(CurrentEnergyConsumer);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deleting current object EnergyConsumer
        /// </summary>
        /// <exception cref="ObjectHasReferenceException"/>
        public void Delete()
        {
            using (var db = new ApplicationsContext())
            {
                db.EnergyConsumers.Attach(CurrentEnergyConsumer);
                db.EnergyConsumers.Remove(CurrentEnergyConsumer);
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