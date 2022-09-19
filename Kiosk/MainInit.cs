using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Kiosk
{
    class MainInit : Form
    {
        public static bool main_InitOK = false;
        private SplashInit splashInit = new SplashInit();
        private DBControl.StoreDAO storeDao = new DBControl.StoreDAO();
        private DBControl.MenuGrpCfgDAO menuGrpDao = new DBControl.MenuGrpCfgDAO();
        private DBControl.MenuListCfgDAO menuListCfgDao = new DBControl.MenuListCfgDAO();
        private DBControl.MenuRepDAO menuRepDao = new DBControl.MenuRepDAO();
        private DBControl.ProductDAO productDao = new DBControl.ProductDAO();

        public FileInfo fileInfo = new FileInfo(Application.ExecutablePath);
        public static string iniPath = string.Empty; 

        public MainInit()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainInit
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "MainInit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.FormClosing += new FormClosingEventHandler(this.MainInit_FormClosing);
            this.Load += new EventHandler(this.MainInit_Load);
            this.ResumeLayout(false);

        }

        private void MainInit_Load(object sender, EventArgs e)
        {
            InitRun();
        }

        private void InitRun()
        {
            iniPath = fileInfo.Directory.FullName + @"\System\conf\setting.ini";

            if (UtilHelper.IsConnectedToInternet())
            {
                if (!File.Exists(@"System\conf\setting.ini"))
                {
                    FrmErrorBox errBox = new FrmErrorBox("무결성 실패", "프로그램 무결성 검증에 실패하여\n\n 실행할 수 없습니다.");

                    if (errBox.ShowDialog() == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
                else if(!File.Exists(@"Data\Kiosk_Local.mdf"))
                {
                    FrmErrorBox errBox = new FrmErrorBox("무결성 실패", "프로그램 무결성 검증에 실패하여\n\n 실행할 수 없습니다.");

                    if (errBox.ShowDialog() == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    SettingLoad();
                    if (IsGetStoreInfo())
                    {
                        if (ServerDataSync())
                        {
                            main_InitOK = true;
                        }
                    }
                    this.Dispose();
                }

            }
            else
            {
                FrmNetworkErr networkErr = new FrmNetworkErr();

                if (networkErr.ShowDialog() == DialogResult.OK)
                    Application.Exit();
            }
        }

        private bool IsGetStoreInfo()
        {
            storeDao.BRAND = StoreInfo.BrnadCode;
            storeDao.STORE = StoreInfo.StoreCode;
            storeDao.DESK = StoreInfo.StoreDesk;
            bool result = false;
            try
            {
                using (DataTable dt = storeDao.StoreCodeSelect())
                {

                    if (dt.Rows.Count != 0)
                    {
                        using (DataTableReader dtr = new DataTableReader(dt))
                        {
                            while (dtr.Read())
                            {
                                StoreInfo.StoreName = dtr["MS_NAME"].ToString();
                                StoreInfo.StoreArea = dtr["MS_AREA"].ToString();
                                if (dtr["MS_BSID"].ToString().Length >= 10)
                                    StoreInfo.StoreSano = dtr["MS_BSID"].ToString().Insert(3, "-").Insert(6, "-");
                                else
                                    StoreInfo.StoreSano = dtr["MS_BSID"].ToString();
                                StoreInfo.StoreSang = dtr["MS_BSNM"].ToString();
                                StoreInfo.StoreCeo = dtr["MS_CEO"].ToString();
                                StoreInfo.StoreAdd1 = dtr["MS_ADD1"].ToString();
                                StoreInfo.StoreAdd2 = dtr["MS_ADD2"].ToString();
                                StoreInfo.StorePhon = dtr["MS_PHON"].ToString();

                            }
                            dtr.Close();
                        }
                        result = true;
                    }

                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return result;
        }

        private bool ServerDataSync()
        {
            productDao.BRAND = menuRepDao.BRAND = menuListCfgDao.BRAND = menuGrpDao.BRAND = StoreInfo.BrnadCode;
            //productDao.BRAND = menuRepDao.BRAND = menuListCfgDao.BRAND = menuGrpDao.BRAND = "00002";
            productDao.STORE = menuRepDao.STORE = menuListCfgDao.STORE = menuGrpDao.STORE = StoreInfo.StoreCode;
            productDao.DESK =  menuRepDao.DESK = menuListCfgDao.DESK =  menuGrpDao.DESK = StoreInfo.StoreDesk;

            try
            {
                using (DataTable dt = menuGrpDao.MenuGrpCfgSelect())
                {
                    if (dt.Rows.Count != 0)
                    {
                        using (DataTableReader dtr = new DataTableReader(dt))
                        {
                            menuGrpDao.MenuGrpCfgDeleteL();

                            while (dtr.Read())
                            {
                                menuGrpDao.SEQN = dtr["MG_SEQN"].ToString();
                                menuGrpDao.NAME = dtr["MG_NAME"].ToString();
                                menuGrpDao.TYPE = dtr["MG_TYPE"].ToString();
                                menuGrpDao.DDAT = Convert.ToDateTime(dtr["MG_DDAT"].ToString());

                                menuGrpDao.MenuGrpCfgSaveL();
                            }

                            dtr.Close();
                        }
                    }
                }

                using (DataTable dt = menuListCfgDao.MenuListCfgAllSelect())
                {
                    if (dt.Rows.Count != 0)
                    {
                        using (DataTableReader dtr = new DataTableReader(dt))
                        {
                            menuListCfgDao.MenuListCfgDeleteL();

                            while (dtr.Read())
                            {
                                menuListCfgDao.GNAM = dtr["MT_GNAM"].ToString();
                                menuListCfgDao.TYPE = dtr["MT_TYPE"].ToString();
                                menuListCfgDao.PAGE = dtr["MT_PAGE"].ToInteger();
                                menuListCfgDao.COLROW = dtr["MT_COLROW"].ToInteger();
                                menuListCfgDao.TNAM = dtr["MT_TNAM"].ToString();
                                menuListCfgDao.TCOD = dtr["MT_TCOD"].ToString();
                                menuListCfgDao.DDAT = Convert.ToDateTime(dtr["MT_DDAT"].ToString());
                                menuListCfgDao.SOLDYN = dtr["MT_SOLDYN"].ToString();

                                menuListCfgDao.MenuListCfgSaveL();
                            }

                            dtr.Close();
                        }
                    }
                }

                using (DataTable dt = menuRepDao.MenuRepAllSelect())
                {
                    if (dt.Rows.Count != 0)
                    {
                        using (DataTableReader dtr = new DataTableReader(dt))
                        {
                            menuRepDao.MenuRepAllDeleteL();

                            while (dtr.Read())
                            {
                                menuRepDao.CODE = dtr["MR_CODE"].ToString();
                                menuRepDao.NAME = dtr["MR_NAME"].ToString();
                                menuRepDao.GNAM = dtr["MR_GNAM"].ToString();
                                menuRepDao.IMGNM = dtr["MR_IMGNM"].ToString();
                                menuRepDao.MEMO = dtr["MR_MEMO"].ToString();
                                menuRepDao.SEQN = dtr["MR_SEQN"].ToString();
                                menuRepDao.IMAGE = (byte[])dtr["MR_IMAGE"];
                                menuRepDao.DDAT = Convert.ToDateTime(dtr["MR_DDAT"].ToString());

                                if (dtr["MR_UPDT"] != DBNull.Value)
                                    menuRepDao.UPDT = Convert.ToDateTime(dtr["MR_UPDT"].ToString());

                                menuRepDao.MenuRepSaveL();
                            }
                            dtr.Close();
                        }
                    }
                }

                using (DataTable dt = productDao.ProductAllSelect())
                {
                    if (dt.Rows.Count != 0)
                    {
                        using (DataTableReader dtr = new DataTableReader(dt))
                        {
                            productDao.ProductAllDeleteL();

                            while (dtr.Read())
                            {
                                productDao.CODE = dtr["PT_CODE"].ToString();
                                productDao.RCOD = dtr["PT_RCOD"].ToString();
                                productDao.TCOD = dtr["PT_TCOD"].ToString();
                                productDao.NAME = dtr["PT_NAME"].ToString();
                                productDao.OPNM = dtr["PT_OPNM"].ToString();
                                productDao.GCOD = dtr["PT_GCOD"].ToString();
                                productDao.GNAM = dtr["PT_GNAM"].ToString();
                                productDao.PRICE = dtr["PT_PRICE"].ToDouble();
                                productDao.MEMO = dtr["PT_MEMO"].ToString();
                                productDao.SEQN = dtr["PT_SEQN"].ToString();
                                productDao.DDAT = Convert.ToDateTime(dtr["PT_DDAT"].ToString());

                                productDao.ProductSaveL();
                            }

                            dtr.Close();
                        }
                    }
                }

                using (DataTable dt = menuListCfgDao.SetMenuListCfgAllSelect())
                {
                    if (dt.Rows.Count != 0)
                    {
                        using (DataTableReader dtr = new DataTableReader(dt))
                        {
                            menuListCfgDao.SetMenuListCfgAllDeleteL();

                            while (dtr.Read())
                            {
                                menuListCfgDao.PAGE = dtr["ST_PAGE"].ToInteger();
                                menuListCfgDao.COLROW = dtr["ST_COLROW"].ToInteger();
                                menuListCfgDao.NAME = dtr["ST_NAME"].ToString();
                                menuListCfgDao.PRICE = dtr["ST_PRICE"].ToString();
                                menuListCfgDao.TCOD = dtr["ST_TCOD"].ToString();
                                menuListCfgDao.DDAT = Convert.ToDateTime(dtr["ST_DDAT"].ToString());

                                menuListCfgDao.SetMenuListCfgSaveL();
                            }

                            dtr.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return true;
        }

        public static void SettingLoad()
        {
            StoreInfo.BrnadCode = UtilHelper.GetiniValue("STOREINFO", "BRAND", iniPath);
            StoreInfo.StoreCode = UtilHelper.GetiniValue("STOREINFO", "STORE", iniPath);
            StoreInfo.StoreDesk = UtilHelper.GetiniValue("STOREINFO", "KIONO", iniPath);
            StoreInfo.IsCur = UtilHelper.GetiniValue("STOREINFO", "CUR", iniPath) == "1" ? true : false;
            StoreInfo.Kitchen1Prn = UtilHelper.GetiniValue("PRINT", "KITCHEN1PRN", iniPath);
            StoreInfo.Kitchen2Prn = UtilHelper.GetiniValue("PRINT", "KITCHEN2PRN", iniPath);
            StoreInfo.ReceiptPrn = UtilHelper.GetiniValue("PRINT", "RECEIPTPRN", iniPath);
            StoreInfo.CounterPrn = UtilHelper.GetiniValue("PRINT", "COUNTERPRN", iniPath);
            StoreInfo.Kitchen1Rate = UtilHelper.GetiniValue("PRINT", "KITCHEN1RATE", iniPath).ToInteger();
            StoreInfo.Kitchen2Rate = UtilHelper.GetiniValue("PRINT", "KITCHEN2RATE", iniPath).ToInteger();
            StoreInfo.ReceiptRate = UtilHelper.GetiniValue("PRINT", "RECEIPTRATE", iniPath).ToInteger();
            StoreInfo.CounterRate = UtilHelper.GetiniValue("PRINT", "COUNTERRATE", iniPath).ToInteger();
            StoreInfo.CounterType = UtilHelper.GetiniValue("PRINT", "COUNTERTYPE", iniPath);
            StoreInfo.Kitchen1Type = UtilHelper.GetiniValue("PRINT", "KITCHEN1TYPE", iniPath);
            StoreInfo.Kitchen2Type = UtilHelper.GetiniValue("PRINT", "KITCHEN2TYPE", iniPath);
            StoreInfo.CounterPrnIP1 = UtilHelper.GetiniValue("PRINT", "COUNTERPRN_IP", iniPath);
            StoreInfo.KitchenPrnIP1 = UtilHelper.GetiniValue("PRINT", "KITCHENPRN_IP", iniPath);
            StoreInfo.KitchenPrnIP2 = UtilHelper.GetiniValue("PRINT", "KITCHENPRN1_IP", iniPath);
            StoreInfo.CounterPrnPort1 = UtilHelper.GetiniValue("PRINT", "COUNTERPRN_PORT", iniPath).ToInteger();
            StoreInfo.KitchenPrnPort1 = UtilHelper.GetiniValue("PRINT", "KITCHENPRN_PORT", iniPath).ToInteger();
            StoreInfo.KitchenPrnPort2 = UtilHelper.GetiniValue("PRINT", "KITCHENPRN1_PORT", iniPath).ToInteger();
            StoreInfo.IsHoldMode = UtilHelper.GetiniValue("HOLDMODE", "FLAG", iniPath) == "1" ? true : false;
            StoreInfo.VanSelect = UtilHelper.GetiniValue("VAN", "FLAG", iniPath);
            StoreInfo.MediaVolume = UtilHelper.GetiniValue("MOVIEVOL", "VOLUME", iniPath).ToInteger();
            StoreInfo.BWaitTime = UtilHelper.GetiniValue("STOREINFO", "BWAITTIME", iniPath).ToInteger();
            StoreInfo.PWaitTiem = UtilHelper.GetiniValue("STOREINFO", "PWAITTIME", iniPath).ToInteger();
            StoreInfo.Tid = UtilHelper.GetiniValue("VAN", "TID", iniPath);
            StoreInfo.Port = UtilHelper.GetiniValue("VAN", "PORT", iniPath);
            StoreInfo.MaxTap = UtilHelper.GetiniValue("TAP", "MAX", iniPath).ToInteger();
            StoreInfo.ExceptMenuGrp = UtilHelper.GetiniValue("EXCEPTMENU", "MENUGRP", iniPath).Split(',');
            StoreInfo.ExceptMenu = UtilHelper.GetiniValue("EXCEPTMENU", "MENU", iniPath).Split(',');
            StoreInfo.CuttingMenu = UtilHelper.GetiniValue("CUTTING", "MENU", iniPath).Split(',');
        }

        private void MainInit_FormClosing(object sender, FormClosingEventArgs e)
        {
            storeDao = null;
            menuGrpDao = null;
            menuListCfgDao = null;
            menuRepDao = null;
            productDao = null;
        }
    }
}
