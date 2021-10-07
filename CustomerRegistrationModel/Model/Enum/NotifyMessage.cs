using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerRegistrationModel.Enum
{
    public enum NotifyMessage
    {
        // product
        /// <summary>{0} item {1} planning is changed.</summary>
        [DisplayAttribute(Description = "{0} item {1} planning is changed.")]
        MP001,
        /// <summary>{0} planning is changed.</summary>
        [DisplayAttribute(Description = "{0} planning is changed.")]
        MP002,
        /// <summary>{0} Product Code: {1} weight {2} change to {3}.</summary>
        [DisplayAttribute(Description = "{0} Product Code: {1} weight {2} change to {3}.")]
        MP003,
        /// <summary>{0} Product Code: {1} length weight {2}, received production length {3}.</summary>
        [DisplayAttribute(Description = "{0} Product Code: {1} length weight {2}, received production length {3}.")]
        MP004,
        ///// <summary>{0} Product Code: {1} has received the process.</summary>
        //[DisplayAttribute(Description = "{0} Product Code: {1} has received the process.")]
        //MP004,

        // cost & price
        /// <summary>{0} cost update completed.</summary>
        [DisplayAttribute(Description = "{0} cost update completed.")]
        MC000,        
        /// <summary>{0} item {1} cost updated.</summary>
        [DisplayAttribute(Description = "{0} item {1} cost updated.")]
        MC001,
        /// <summary>{0} item {1} cost is changed.</summary>
        [DisplayAttribute(Description = "{0} item {1} cost is changed.")]
        MC002,
        /// <summary>{0} price update completed.</summary>
        [DisplayAttribute(Description = "{0} price update completed.")]
        MC003,
        /// <summary>{0} item {1} price updated.</summary>
        [DisplayAttribute(Description = "{0} item {1} price updated.")]
        MC004,

        // quotation
        /// <summary>{0} item {1} pending approval.</summary>
        [DisplayAttribute(Description = "{0} item {1} pending approval.")]
        MQ000,
        /// <summary>{0} pending approval.</summary>
        [DisplayAttribute(Description = "{0} pending approval.")]
        MQ001,
        /// <summary>{0} item {1} approved.</summary>
        [DisplayAttribute(Description = "{0} item {1} approved.")]
        MQ002,
        /// <summary>{0} approved.</summary>
        [DisplayAttribute(Description = "{0} approved.")]
        MQ003,
        /// <summary>{0} item {1} not approved. {2}</summary>
        [DisplayAttribute(Description = "{0} item {1} not approved. {2}")]
        MQ004,
        /// <summary>{0} not approved. {2}</summary>
        [DisplayAttribute(Description = "{0} not approved. {2}")]
        MQ005,

        // commission
        /// <summary>{0} | {1} pending approval.</summary>
        [DisplayAttribute(Description = "{0} | {1} pending approval.")]
        MM000,
        /// <summary>{0} | {1} approved.</summary>
        [DisplayAttribute(Description = "{0} | {1} approved.")]
        MM001,
        /// <summary>{0} | {1} not approved. {2}</summary>
        [DisplayAttribute(Description = "{0} | {1} not approved. {2}")]
        MM002,

        // commission
        /// <summary>{0} pending approval.</summary>
        [DisplayAttribute(Description = "{0} | {1} pending approval.")]
        MT000,
        /// <summary>{0} approved.</summary>
        [DisplayAttribute(Description = "{0} | {1} approved.")]
        MT001,
        /// <summary>{0} not approved. {1}</summary>
        [DisplayAttribute(Description = "{0} | {1} not approved. {2}")]
        MT002,

        // order
        /// <summary>{0} pending approval.</summary>
        [DisplayAttribute(Description = "{0} pending approval. {1}")]
        MO000,
        /// <summary>{0} pending special approval.</summary>
        [DisplayAttribute(Description = "{0} pending special approval. {1}")]
        MO001,
        /// <summary>{0} approved completed.</summary>
        [DisplayAttribute(Description = "{0} approved completed.")]
        MO002,
        /// <summary>{0} not approved. {2}</summary>
        [DisplayAttribute(Description = "{0} not approved. {1}")]
        MO003,

        // revsion
        /// <summary>{0} revision {1} created.</summary>
        [DisplayAttribute(Description = "{0} revision {1} created.")]
        MR000,

        // deposit
        /// <summary>{0} must pay a deposit before production.</summary>
        [DisplayAttribute(Description = "{0} must pay a deposit before production.")]
        MD000,
    }
}
