using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekat_forme_izgled
{
    public partial class descriptions : Form
    {
        public descriptions(int i) // 1 otpornik, 2 struja, 3 napon
        {
            InitializeComponent();
            StringBuilder sb = new StringBuilder();
            switch (i)
            {
                case 1:
                    sb.AppendLine("Отпорник (енгл. resistor) је пасивна електронска компонента са два извода (једним приступом) која пружа отпор струји, стварајући притом пад напона између прикључака. ");
                    sb.AppendLine("Пружање отпора струји као основна особина отпорника описује се електричним отпором. Према Омовом закону електрични отпор једнак је паду напона на отпорнику  ");
                    sb.AppendLine("подељеном са јачином струје која протиче кроз отпорник. Другим речима, отпор је константа сразмере између напона и струје отпорника.");
                    sb.AppendLine("Отпорник се користи као елемент електричних мрежа и електронских уређаја.");
                    break;
                case 2:
                    sb.AppendLine("Електрична струја је усмерено кретање наелектрисања под утицајем електричног поља или разлике електричних потенцијала.");
                    sb.AppendLine("СИ јединица за електричну струју је ампер (А), што је једнако протоку једног кулона наелектрисања у секунди.");
                    sb.AppendLine("Електрична струја може бити једносмерна или наизменична.[3][4] Струја тече кроз метале, електролите, гасове и полупроводнике.");
                    break;
                case 3:
                    sb.AppendLine("Електрични напон је разлика електричних потенцијала између две тачке у простору. Изражава се у волтима. ");
                              break;
            }
           
           
            textBox1.Text += sb.ToString();
            textBox1.Enabled = false;
        }
       
    }
}
