using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerRegistrationModel.Model.Enum
{
    public enum ErrorCode
    {
        [DisplayAttribute(Description = "Unknown.")]
        U000,

        /******Oauth*******/
        /// <summary>You do not have access.</summary>
        [DisplayAttribute(Description = "You do not have access.")]
        O000,

        /// <summary>You logout.</summary>
        [DisplayAttribute(Description = "You logout.")]
        O001,

        /// <summary>Login Time out.</summary>
        [DisplayAttribute(Description = "Login Time out.")]
        O002,

        /******Permission*******/
        /// <summary>You don't have permission.</summary>
        [DisplayAttribute(Description = "You don't have permission.")]
        P000,

        /// <summary>You don't have permission to approve.</summary>
        [DisplayAttribute(Description = "You don't have permission to approve")]
        P001,

        /******Valid*******/
        /// <summary>Please check I'm not a robot.</summary>
        [DisplayAttribute(Description = "Please check I'm not a robot.")]
        V000,

        /// <summary>Duplicate data.</summary>
        [DisplayAttribute(Description = "Duplicate data.")]
        V001,

        /// <summary>Save failed.</summary>
        [DisplayAttribute(Description = "Save failed.")]
        V002,

        /// <summary>Delete failed.</summary>
        [DisplayAttribute(Description = "Delete failed.")]
        V003,

        /// <summary>Email Not Found.</summary>
        [DisplayAttribute(Description = "Email Not Found.")]
        V004,

        /// <summary>Token reset password expire.</summary>
        [DisplayAttribute(Description = "Token reset password expire.")]
        V005,

        /// <summary>Username not found.</summary>
        [DisplayAttribute(Description = "Username not found.")]
        V006,

        /// <summary>Password and Confirm Password not match.</summary>
        [DisplayAttribute(Description = "Password and Confirm Password not match.")]
        V007,

        /// <summary>Old password is incorrect.</summary>
        [DisplayAttribute(Description = "Old password is incorrect.")]
        V008,

        /// <summary>Username is not Confirm.</summary>
        [DisplayAttribute(Description = "Username is not Confirm.")]
        V009,

        /// <summary>Username or Password was incorrect.</summary>
        [DisplayAttribute(Description = "Username or Password was incorrect.")]
        V010,

        /// <summary>2FA Code invalid..</summary>
        [DisplayAttribute(Description = "2FA Code invalid..")]
        T001,

        /// <summary>Connect Server lost.</summary>
        [DisplayAttribute(Description = "Connect Server lost.")]
        C000,

        /// <summary>Date Overlap.</summary>
        [DisplayAttribute(Description = "Date Overlap.")] //มีช่วงเวลาของ DateFrom กับ DateTo คาบเกี่ยวกัน
        D001,

        /// <summary>Data used. Can't be deleted.</summary>
        [DisplayAttribute(Description = "Data used. Can't be deleted.")]
        D000,

        /// <summary>Data used. Proforma Invoice No. {0}</summary>
        [DisplayAttribute(Description = "Data used. Proforma Invoice No. {0}")]
        D002,

        /// <summary>ไม่พบข้อมูลเทียบเนื้ออวนรุม กรุณาแจ้งให้ผลิตเทียบเนื้ออวน</summary>
        [DisplayAttribute(Description = "ไม่พบข้อมูลเทียบเนื้ออวนรุม กรุณาแจ้งให้ผลิตเทียบเนื้ออวน")]
        D003,

        /// <summary>There is active data, unable to save data.</summary>
        [DisplayAttribute(Description = "There is active data, unable to save data.")] //มีข้อมูลที่ใช้งานอยู่, ไม่สามารถบันทึกข้อมูลได้
        W000,

        /// <summary>There is used in quotation, unable to save data.</summary>
        [DisplayAttribute(Description = "There is used in quotation, unable to save data.")] //มีการใช้ในใบเสนอราคาไม่สามารถบันทึกข้อมูลได้
        W001,

        /// <summary>Generate code running overflow.</summary>
        [DisplayAttribute(Description = "Generate code running overflow. {0}")]
        E000,

        /* Production Planning */
        /// <summary>ไม่สามารถติดต่อโปรแกรมวางแผนผลิต PPS ได้</summary>
        [DisplayAttribute(Description = "ไม่สามารถติดต่อโปรแกรมวางแผนผลิต PPS ได้")]
        PP00,
        /// <summary>ไม่สามารถแก้ไขข้อมูลได้เนื่องจากติดเงื่อนไขการวางแผนที่โปรแกรมวางแผนผลิต PPS กรุณาแจ้งผลิตให้รับทราบการปรับแก้ไขข้อมูลก่อน</summary>
        [DisplayAttribute(Description = "ไม่สามารถแก้ไขข้อมูลได้เนื่องจากติดเงื่อนไขการวางแผนที่โปรแกรมวางแผนผลิต PPS กรุณาแจ้งผลิตให้รับทราบการปรับแก้ไขข้อมูลก่อน")]
        PP01,
        /// <summary>ไม่สามารถลบข้อมูลได้เนื่องจากติดเงื่อนไขการวางแผนที่โปรแกรมวางแผนผลิต PPS กรุณาแจ้งผลิตให้รับทราบการยกเลิกแผนผลิตก่อน</summary>
        [DisplayAttribute(Description = "ไม่สามารถลบข้อมูลได้เนื่องจากติดเงื่อนไขการวางแผนที่โปรแกรมวางแผนผลิต PPS กรุณาแจ้งผลิตให้รับทราบการยกเลิกแผนผลิตก่อน")]
        PP02,

    }
}
