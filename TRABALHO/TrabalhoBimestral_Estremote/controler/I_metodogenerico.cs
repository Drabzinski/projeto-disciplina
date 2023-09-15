using System;

namespace TrabalhoBimestral_Estremote.controler
{
    internal interface I_metodogenerico
    {
        void inserirDados(Object obj);

        void apagarDados(int valor);

        void editarDados(Object obj);

        void consultarTodos();

        Object buscarId(int valor);
    }
}