using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWebSena.Models.Paginador
{
    public class Paginador<T>
    {
        //cantidad de resultados por página 
        private int pagi_cuantos = 3;
        //cantidad de enlaces que se mostrarán como máximo en la barra de navegación 
        private int pagi_nav_num_enlaces = 5;
        private int pagi_actual;
        //definimos qué irá en el enlace a la página anterior 
        private String pagi_nav_anterior = " &laquo; Anterior ";
        //definimos qué irá en el enlace a la página siguiente 
        private String pagi_nav_siguiente = " Siguiente &raquo; ";
        //definimos qué irá en el enlace a la página siguiente 
        private String pagi_nav_primera = " &laquo; Primero ";
        private String pagi_nav_ultima = " Último &raquo; ";
        private String pagi_navegacion = null;

        public object[] paginador(List<T> table, int pagina, int Registros, String area, String controller,
            String action, String host)
        {
            if (Registros > 0)
            {
                pagi_cuantos = Registros;
            }
            if (pagina.Equals(0))
            {
                // Si no se ha hecho click a ninguna página específica 
                // O sea si es la primera vez que se ejecuta el script 
                // pagi_actual es la página actual-->será por defecto la primera. 
                pagi_actual = 1;
            }
            else
            {
                // Si se "pidió" una página específica: 
                // La página actual será la que se pidió. 
                pagi_actual = pagina;
            }
            int pagi_totalReg = table.Count;
            int pagi_totalRegs = pagi_totalReg;
            if ((pagi_totalReg % pagi_cuantos) > 0)
            {
                pagi_totalRegs += 2;
            }
            int pagi_totalPags = pagi_totalRegs / pagi_cuantos;
            if (pagi_actual != 1)
            {
                // Si no estamos en la página 1. Ponemos el enlace "primera" 
                int pagi_url = 1; //será el número de página al que enlazamos 
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + area + "/" + controller + "/" + action
                    + "?Pag=" + pagi_url + "&Registros=" + pagi_cuantos + "'>" + pagi_nav_primera + "</a>";

                // Si no estamos en la página 1. Ponemos el enlace "anterior" 
                pagi_url = pagi_actual - 1; //será el número de página al que enlazamos 
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + area + "/" + controller + "/" + action
                    + "?Pag=" + pagi_url + "&Registros=" + pagi_cuantos + "'>" + pagi_nav_anterior + " </a>";
            }
            // Si se definió la variable pagi_nav_num_enlaces 
            // Calculamos el intervalo para restar y sumar a partir de la página actual 
            double valor2 = (pagi_nav_num_enlaces / 2);
            int pagi_nav_intervalo = Convert.ToInt16(Math.Round(valor2));
            // Calculamos desde qué número de página se mostrará 
            int pagi_nav_desde = pagi_actual - pagi_nav_intervalo;
            // Calculamos hasta qué número de página se mostrará 
            int pagi_nav_hasta = pagi_actual + pagi_nav_intervalo;
            // Si pagi_nav_desde es un número negativo
            if (pagi_nav_desde < 1)
            {
                // Le sumamos la cantidad sobrante al final para mantener
                //el número de enlaces que se quiere mostrar.  
                pagi_nav_hasta -= (pagi_nav_desde - 1);
                // Establecemos pagi_nav_desde como 1. 
                pagi_nav_desde = 1;
            }
            // Si pagi_nav_hasta es un número mayor que el total de páginas 
            if (pagi_nav_hasta > pagi_totalPags)
            {
                // Le restamos la cantidad excedida al comienzo para mantener 
                //el número de enlaces que se quiere mostrar. 
                pagi_nav_desde -= (pagi_nav_hasta - pagi_totalPags);
                // Establecemos pagi_nav_hasta como el total de páginas. 
                pagi_nav_hasta = pagi_totalPags;
                // Hacemos el último ajuste verificando que al cambiar pagi_nav_desde 
                //no haya quedado con un valor no válido. 
                if (pagi_nav_desde < 1)
                {
                    pagi_nav_desde = 1;
                }
            }
            for (int pagi_i = pagi_nav_desde; pagi_i <= pagi_nav_hasta; pagi_i++)
            {
                //Desde página 1 hasta última página (pagi_totalPags) 
                if (pagi_i == pagi_actual)
                {
                    // Si el número de página es la actual (pagi_actual). Se escribe el número, pero sin enlace y en negrita. 
                    pagi_navegacion += "<span class='btn btn-default' disabled='disabled'>" + pagi_i + "</span>";
                }
                else
                {
                    // Si es cualquier otro. Se escribe el enlace a dicho número de página. 
                    pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + area + "/" + controller + "/" +
                        action + "?Pag=" + pagi_i + "&Registros=" + pagi_cuantos + "'>" +
                        pagi_i + " </a>";
                }
            }
            if (pagi_actual < pagi_totalPags)
            {
                // Si no estamos en la última página. Ponemos el enlace "Siguiente" 
                int pagi_url = pagi_actual + 1; //será el número de página al que enlazamos 
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + area  + "/"+ controller + "/" +
                    action + "?Pag=" + pagi_url + "&Registros=" + pagi_cuantos +  "'>" +
                    pagi_nav_siguiente + "</a>";

                // Si no estamos en la última página. Ponemos el enlace "Última" 
                pagi_url = pagi_totalPags; //será el número de página al que enlazamos 
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + area +"/" + controller + "/" +
                    action + "?Pag=" + pagi_url + "&Registros=" + pagi_cuantos +  "'>" +
                    pagi_nav_ultima + "</a>";
            }
            /* 
        * Obtención de los registros que se mostrarán en la página actual. 
        *------------------------------------------------------------------------ 
        */
            // Calculamos desde qué registro se mostrará en esta página 
            // Recordemos que el conteo empieza desde CERO. 
            int pagi_inicial = (pagi_actual - 1) * pagi_cuantos;
            // Consulta SQL. Devuelve cantidad registros empezando desde pagi_inicial

            var query = table.Skip(pagi_inicial).Take(pagi_cuantos).ToList();

            String pagi_info = " del <b>" + pagi_actual + "</b> al <b>" + pagi_totalPags + "</b> de <b>" +
                pagi_totalReg + "</b> <b>/" + pagi_cuantos + " registros por pagina </b>";

            if (query == null || query.Count == 0)
            {
                pagi_info = "0 Registros";
                pagi_navegacion = "";
            }

            object[] data = { pagi_info, pagi_navegacion, query };
            return data;
        }

    }
}
