using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M17E_TrabalhoModelo_2021_22.Helper
{
    public static class Utils
    {
        public static string UserId(this HtmlHelper htmlHelper,
            System.Security.Principal.IPrincipal utilizador)
        {
            string iduser = "";

            using (var context = new M17E_TrabalhoModelo_2021_22.Data.M17E_TrabalhoModelo_2021_22Context())
            {
                var consulta = context.Database.SqlQuery<int>(
                    "SELECT UserID FROM Users WHERE nome=@p0",
                    utilizador.Identity.Name
                    );
                if (consulta.ToList().Count > 0)
                    iduser = consulta.ToList()[0].ToString();
            }

            return iduser;
        }
    }
}