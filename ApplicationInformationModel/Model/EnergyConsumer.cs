﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationInformationModel.Model
{
    /// <summary>
    /// Generic user of energy - a point of consumption on the power system model
    /// </summary>
    public class EnergyConsumer:IdentifiedObject
    {
        /// <summary>
        /// Number of individual customers represented by this Demand
        /// </summary>
        public int CustomerCount { get; set; }

        /// <summary>
        /// Active power of the load that is a fixed quantity
        /// </summary>
        public double Pfixed { get; set; }

        /// <summary>
        /// Fixed active power as per cent of load group fixed active power
        /// </summary>
        public double PfixedPct { get; set; }

        /// <summary>
        /// Reactive power of the load that is a fixed quantity
        /// </summary>
        public double Qfixed { get; set; }

        /// <summary>
        /// Fixed reactive power as per cent of load group fixed reactive power.
        /// </summary>
        public double QfixedPct { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? Substation_MRID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("Substation_MRID")]
        public Substation Substation { get; set; }


        /// <summary>
        /// Collection of object of class StaticLoadCharacteristic
        /// </summary>
        public virtual ICollection<StaticLoadCharacteristic> StaticLoadCharacteristics { get; set; }

        /// <summary>
        /// Collection of object of class Analog
        /// </summary>
        public virtual ICollection<Analog> Analogs { get; set; }

        /// <summary>
        /// No parameterless constructor of class EnergyConsumer
        /// </summary>
        public EnergyConsumer() { }

        /// <summary>
        /// Constructor of class EnergyConsumer
        /// </summary>
        /// <param name="mRid">A Model Authority issues mRIDs</param>
        /// <param name="name">The name is a free text human readable name of the object</param>
        /// <param name="customerCount">Number of individual customers represented by this Demand</param>
        /// <param name="qfixed">Reactive power of the load that is a fixed quantity</param>
        /// <param name="pfixedPct">Fixed active power as per cent of load group fixed active power</param>
        /// <param name="qfixedPct">Fixed reactive power as per cent of load group fixed reactive power</param>
        /// <param name="pfixed">Active power of the load that is a fixed quantity</param>
        public EnergyConsumer(Guid mRid, string name, int customerCount, double pfixed, double qfixed, double pfixedPct, double qfixedPct) : base(mRid, name)
        {
            #region CheckingInputArguments
            if (pfixedPct < 0)
            {
                throw new ArgumentOutOfRangeException("Value of pfixedPct mustn`t be less than 0", nameof(pfixedPct));
            }
            if (qfixedPct < 0)
            {
                throw new ArgumentOutOfRangeException("Value of qfixedPct mustn`t be less than 0", nameof(qfixedPct));
            }
            if (pfixed < 0)
            {
                throw new ArgumentOutOfRangeException("Value of pfixed mustn`t be less than 0", nameof(pfixed));
            }
            if (qfixed < 0)
            {
                throw new ArgumentOutOfRangeException("Value of qfixed mustn`t be less than 0", nameof(qfixed));
            }
            #endregion
            this.CustomerCount = customerCount;
            this.Pfixed = pfixed;
            this.Qfixed = qfixed;
            this.PfixedPct = pfixedPct;
            this.QfixedPct = qfixedPct;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="substation"></param>
        public void SetSubstation(Substation substation)
        {
            Substation_MRID = substation.MRID;
            Substation = substation;
        }

    }
}