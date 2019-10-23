using System;
using System.Collections.Generic;
using System.Text;


namespace App4
{

    public partial class Resultado
    {

        private Servico[] servicosField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public Servico[] Servicos
        {
            get
            {
                return this.servicosField;
            }
            set
            {
                this.servicosField = value;
            }
        }
    }


    public partial class Servico
    {

        private int codigoField;

        private string valorField;

        private string prazoEntregaField;

        private string valorMaoPropriaField;

        private string valorAvisoRecebimentoField;

        private string valorValorDeclaradoField;

        private string entregaDomiciliarField;

        private string entregaSabadoField;

        private string erroField;

        private string msgErroField;

        private string valorSemAdicionaisField;

        private string obsFimField;

        private string dataMaxEntregaField;

        private string horaMaxEntregaField;

        /// <remarks/>
        public int Codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        /// <remarks/>
        public string Valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
            }
        }

        /// <remarks/>
        public string PrazoEntrega
        {
            get
            {
                return this.prazoEntregaField;
            }
            set
            {
                this.prazoEntregaField = value;
            }
        }

        /// <remarks/>
        public string ValorMaoPropria
        {
            get
            {
                return this.valorMaoPropriaField;
            }
            set
            {
                this.valorMaoPropriaField = value;
            }
        }

        /// <remarks/>
        public string ValorAvisoRecebimento
        {
            get
            {
                return this.valorAvisoRecebimentoField;
            }
            set
            {
                this.valorAvisoRecebimentoField = value;
            }
        }

        /// <remarks/>
        public string ValorValorDeclarado
        {
            get
            {
                return this.valorValorDeclaradoField;
            }
            set
            {
                this.valorValorDeclaradoField = value;
            }
        }

        /// <remarks/>
        public string EntregaDomiciliar
        {
            get
            {
                return this.entregaDomiciliarField;
            }
            set
            {
                this.entregaDomiciliarField = value;
            }
        }

        /// <remarks/>
        public string EntregaSabado
        {
            get
            {
                return this.entregaSabadoField;
            }
            set
            {
                this.entregaSabadoField = value;
            }
        }

        /// <remarks/>
        public string Erro
        {
            get
            {
                return this.erroField;
            }
            set
            {
                this.erroField = value;
            }
        }

        /// <remarks/>
        public string MsgErro
        {
            get
            {
                return this.msgErroField;
            }
            set
            {
                this.msgErroField = value;
            }
        }

        /// <remarks/>
        public string ValorSemAdicionais
        {
            get
            {
                return this.valorSemAdicionaisField;
            }
            set
            {
                this.valorSemAdicionaisField = value;
            }
        }

        /// <remarks/>
        public string obsFim
        {
            get
            {
                return this.obsFimField;
            }
            set
            {
                this.obsFimField = value;
            }
        }

        /// <remarks/>
        public string DataMaxEntrega
        {
            get
            {
                return this.dataMaxEntregaField;
            }
            set
            {
                this.dataMaxEntregaField = value;
            }
        }

        /// <remarks/>
        public string HoraMaxEntrega
        {
            get
            {
                return this.horaMaxEntregaField;
            }
            set
            {
                this.horaMaxEntregaField = value;
            }
        }
    }
}
