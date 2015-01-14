using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Small_World;

namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour UnitBox.xaml
    /// </summary>
    public partial class UnitBox : UserControl
    {
        Unit unit;
        MainWindow mainWindow;

        public UnitBox(Unit u, int unitNo, int playerNo)
        {
            unit = u;
            mainWindow = (Application.Current.MainWindow as MainWindow);
            InitializeComponent();

            unitAttackPt.Tag = unit.AttackPt;
            unitDefensePt.Tag = unit.DefensePt;
            unitHitPt.Tag = unit.HitPt;
            unitPosX.Tag = unit.Position.X;
            unitPosY.Tag = unit.Position.Y;
            unitNumber.Tag = unitNo;
            playerNumber.Tag = playerNo;

            if (unit.MovePt > 0)
            {
                unitImage.Source = new BitmapImage(unit.GetUnitIcon());
            }
            else
            {
                unitImage.Source = new BitmapImage(unit.GetUnactiveUnitIcon());
            }
        }
    }
}
