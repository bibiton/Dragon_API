using Dragon_Library.Models.Entity;
using Dragon_Library.Repository;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Helper.StatusCalc
{
    /// <summary>
    /// 
    /// </summary>
    public static class StatusCalc
    {
        /*
                生命	魔力	攻擊	防禦	敏捷	精神	回復
         體力	+8		+1		+0.1	+0.1	+0.1	-0.3	+0.8
         力量	+2		+2		+2		+0.2	+0.2	-0.1	-0.1
         強度	+3		+2		+0.2	+2		+0.2	+0.2	-0.1
         速度	+3		+2		+0.2	+0.2	+2		-0.1	+0.2
         魔法	+1		+10		+0.1	+0.1	+0.1	+0.8	-0.3
        */

        public static MemberRoleStatus CalcAll(MemberRole memberRole)
        {
            #region 資料庫連線物件
            GenericRepository<MemberRoleStatus> MemberRoleStatus_db = new GenericRepository<MemberRoleStatus>();
            #endregion

            MemberRoleStatus memberRoleStatus = new MemberRoleStatus();
            memberRoleStatus.MemberRole_ID = memberRole.MemberRole_ID;
            #region HP
            memberRoleStatus.HP = memberRole.Con * 8 +
                                  memberRole.Str * 2 +
                                  memberRole.Vit * 3 +
                                  memberRole.Spd * 3 +
                                  memberRole.Int * 1 + 20;
            #endregion
            #region MP
            memberRoleStatus.MP = memberRole.Con * 1 +
                                  memberRole.Str * 2 +
                                  memberRole.Vit * 2 +
                                  memberRole.Spd * 2 +
                                  memberRole.Int * 10 + 20;
            #endregion
            #region Atk
            memberRoleStatus.Atk = memberRole.Con * 1 +
                                  memberRole.Str * 2 +
                                  memberRole.Vit * .2 +
                                  memberRole.Spd * .2 +
                                  memberRole.Int * .1 + 20;
            #endregion
            #region Def
            memberRoleStatus.Def = memberRole.Con * .1 +
                                  memberRole.Str * .2 +
                                  memberRole.Vit * 2 +
                                  memberRole.Spd * .2 +
                                  memberRole.Int * .1 + 20;
            #endregion
            #region Agi
            memberRoleStatus.Agi = memberRole.Con * .1 +
                                  memberRole.Str * .2 +
                                  memberRole.Vit * .2 +
                                  memberRole.Spd * 2 +
                                  memberRole.Int * .1 + 20;
            #endregion
            #region Spi
            memberRoleStatus.Spi = memberRole.Con * -.3 +
                                  memberRole.Str * -.1 +
                                  memberRole.Vit * .2 +
                                  memberRole.Spd * -.1 +
                                  memberRole.Int * +.8 + 100;
            #endregion
            #region Res
            memberRoleStatus.Res = memberRole.Con * .8 +
                                  memberRole.Str * -.1 +
                                  memberRole.Vit * -.1 +
                                  memberRole.Spd * .2 +
                                  memberRole.Int * -.3 + 100;
            #endregion

            MemberRoleStatus sc = MemberRoleStatus_db.Get(e => e.MemberRole_ID == memberRole.MemberRole_ID);
            MemberRoleStatus_db = new GenericRepository<MemberRoleStatus>();
            if (sc == null)
                MemberRoleStatus_db.Create(memberRoleStatus);
            else
            {
                memberRoleStatus.MemberRoleStatus_ID = sc.MemberRoleStatus_ID;

                MemberRoleStatus_db.Update(memberRoleStatus);
            }

            return memberRoleStatus;
        }

    }
}
