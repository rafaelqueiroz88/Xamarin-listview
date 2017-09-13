using Android.App;
using Android.Widget;
using Android.OS;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;

namespace Mautic
{
    [Activity(Label = "Mautic", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private List<string> mItems;
        private ListView segmentosLista;
        private TextView tituloLista;
        private Button ativar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            segmentosLista = FindViewById<ListView>(Resource.Id.lstSegmentos);
            tituloLista = FindViewById<TextView>(Resource.Id.txtTituloLista);
            ativar = FindViewById<Button>(Resource.Id.btnAtivar);

            ativar.Click += Ativar_Click;           
        }

        private void Ativar_Click(object sender, System.EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Server=23.179.0.120;Port=3306;database=mkt_powertic_com_br;User Id=powertic;Password=powertic101;charset=utf8");
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT leadlist_id FROM lead_lists_leads LIMIT 1");
                    //cmd.ExecuteNonQuery();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    mItems = new List<string>();
                    ArrayAdapter<string> mAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);
                    while (rdr.Read())
                    {
                        mAdapter.Add(rdr.ToString());
                    }
                    
                    //segmentosLista.Adapter = mAdapter;
                    //tituloLista.Text = "Mautic conectado com sucesso!";
                }
            }
            catch(MySqlException ex)
            {
                tituloLista.Text = ex.ToString();
            }
            finally
            {
                con.Close();
            }
        }
    }
}