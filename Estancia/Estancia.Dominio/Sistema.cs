namespace Estancia.Dominio;

public class Sistema
{
    private static Sistema instancia;

    public List<Empleado> Empleados { get; private set; } = new List<Empleado>();
    public List<Animal> Animales { get; private set; } = new List<Animal>();
    public List<Potrero> Potreros { get; private set; } = new List<Potrero>();
    public List<Vacuna> Vacunas { get; private set; } = new List<Vacuna>();

    public static Sistema Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new Sistema();
            }
            return instancia;
        }
    }

    private Sistema()
    {
        Precarga();
    }

    public void AltaCapataz(Capataz c)
    {
        c.Validar();
        Empleados.Add(c);
    }

    public void AltaPeon(Peon p)
    {
        p.Validar();
        Empleados.Add(p);
    }

    public void AltaOvino(Ovino o)
    {
        o.Validar();
        Animales.Add(o);
    }
    public void AltaBovino(Bovino b)
    {
        foreach (Animal a in Animales)
        {
            if (a.ID == b.ID)
            {
                throw new ErrorDeValidacion("Ya existe un bovino con esa caravana");
            }
        }

        b.Validar();
        Animales.Add(b);
    }

    public void AltaPotrero(Potrero p)
    {
        p.Validar();
        Potreros.Add(p);
    }

    public void AltaVacuna(Vacuna v)
    {
        v.Validar();
        Vacunas.Add(v);
    }

    public List<Potrero> BuscarPotreros(int hectareas, int capacidad)
    {
        List<Potrero> potreros = new List<Potrero>();

        foreach (Potrero p in Potreros)
        {
            if (p.Hectareas >= hectareas && p.Capacidad >= capacidad)
            {
                potreros.Add(p);
            }
        }

        return potreros;
    }

    public static void EstablecerPrecioLana(double precio)
    {
        Ovino.PrecioKgLana = precio;
    }

    private void Precarga(int ciclos = 0)
    {
        try
        {
            Random rnd = new Random();

            Capataz capataz1 = new Capataz("juan@estancia.com", "12345678", "Juan Capataz", new DateTime(2020, 1, 1), 5);
            Capataz capataz2 = new Capataz("maria@estancia.com", "12345678", "María Capataz", new DateTime(2020, 2, 1), 7);
            AltaCapataz(capataz1);
            AltaCapataz(capataz2);

            List<string> nombres = ["Marcos", "Pedro", "Marta", "Ana", "Luis", "Laura", "Pablo", "Florencia", "Carlos", "Sofía"];
            for (int i = 0; i < nombres.Count; i++)
            {
                Peon peon = new Peon($"{nombres.ElementAt(i).ToLower()}@estancia.com", "12345678", $"{nombres.ElementAt(i)} Peon", new DateTime(rnd.Next(2019, 2024), i + 1, 1), rnd.Next(2).Equals(1));

                for (int j = 0; j < 15; j++)
                {
                    DateTime fechaInicio;
                    DateTime fechaCierre;
                    bool terminada = rnd.Next(2).Equals(1);

                    if (!terminada)
                    {

                        fechaInicio = DateTime.Now.AddDays(rnd.Next(0, 20));
                        fechaCierre = fechaInicio.AddDays(rnd.Next(0, 20));
                    }
                    else
                    {
                        fechaInicio = DateTime.Now.AddDays(-rnd.Next(0, 20));
                        fechaCierre = fechaInicio.AddDays(rnd.Next(0, 20));
                    }

                    Tarea tarea = new Tarea($"Tarea {j}", fechaInicio, fechaCierre, rnd.Next(2).Equals(1) ? capataz1 : capataz2, peon);
                    peon.AltaTarea(tarea);

                    if (terminada)
                    {
                        tarea.Completar("Todo bien");
                    }
                }

                AltaPeon(peon);
            }

            List<string> letras = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];
            for (int i = 0; i < letras.Count; i++)
            {
                Potrero p = new Potrero($"Potrero {letras.ElementAt(i)}", rnd.Next(10, 100), rnd.Next(100, 1000));
                AltaPotrero(p);
            }

            List<List<string>> vacunas = [
                ["Clostridiosis", "Protege contra enfermedades como la enterotoxemia (o enfermedad del sobreengorde) y el carbunco sintomático, causados por diferentes cepas de bacterias del género Clostridium.", "Clostridium"],
                ["Ectima Contagioso", "Protege contra una enfermedad viral de la piel que puede ser altamente contagiosa, caracterizada por lesiones en la boca y en la piel alrededor de los labios, la nariz y las pezuñas.", "Virus de la Orf"],
                ["Enfermedad de la Bursa Infecciosa", "Es importante para prevenir una enfermedad viral que afecta el sistema inmunológico de los jóvenes, provocando una mayor susceptibilidad a otras infecciones.", "Virus de la Bursitis"],
                ["Enterotoxemia", "Protege contra una enfermedad causada por la bacteria Clostridium perfringens, que puede causar infecciones intestinales graves y toxemias.", "Clostridium perfringens"],
                ["Paratuberculosis", "Ayuda a prevenir una enfermedad crónica del tracto digestivo de los ovinos, causada por la bacteria Mycobacterium avium subespecie paratuberculosis, que puede provocar pérdidas económicas significativas en las explotaciones ganaderas.", "Mycobacterium avium subespecie paratuberculosis"],
                ["Fiebre Aftosa", "Protege contra una enfermedad viral altamente contagiosa que afecta a los animales de pezuña hendida, como los bovinos, y puede causar fiebre, ampollas en la boca y las pezuñas, y en casos graves, la muerte.", "FMDV"],
                ["Rinotraqueitis Infecciosa Bovina", "Protege contra una enfermedad respiratoria viral que puede causar problemas respiratorios, fiebre, pérdida de apetito y otros síntomas en el ganado bovino.", "IBR"],
                ["Brucelosis Bovina", "Fundamental para prevenir esta enfermedad bacteriana que afecta y puede causar abortos, infertilidad y problemas reproductivos. Además, la brucelosis es una enfermedad zoonótica, lo que significa que también puede afectar a los humanos.", "Brucella abortus"],
                ["Carbunco Bacteridiano", "Protege a los bovinos contra una enfermedad bacteriana grave causada por Bacillus anthracis. El carbunco bacteridiano puede ser fatal en los bovinos y también puede afectar a otros animales y a los humanos.", "Bacillus anthracis"],
                ["Leptospirosis", "Protege contra una enfermedad bacteriana llamada leptospirosis, que puede afectar a los bovinos y provocar abortos, infertilidad y problemas renales. La leptospirosis también puede transmitirse a los humanos a través del contacto con orina contaminada de animales infectados.", "Leptospira"]];
            foreach (List<string> v in vacunas)
            {
                Vacuna vacuna = new Vacuna(v.ElementAt(0), v.ElementAt(1), v.ElementAt(2));
                AltaVacuna(vacuna);
            }

            for (int i = 0; i < 30; i++)
            {
                // https://www.farmquip.com.uy/blog/204/cuales-son-las-principales-razas-y-donde-se-producen-los-ovinos-en-la-argentina/
                List<string> razas = ["Criolla", "Karakul", "Frisona", "Ideal", "Texel", "Lincoln", "Merino"];

                string razaRandom = razas[rnd.Next(razas.Count)];
                // patron: [R]NNNNNNN
                // nota: probabilidad de colision!
                string idRandom = $"{razaRandom.ElementAt(0)}{rnd.Next(10)}{rnd.Next(10)}{rnd.Next(10)}{rnd.Next(10)}{rnd.Next(10)}{rnd.Next(10)}{rnd.Next(10)}";

                Ovino o = new Ovino(idRandom, razaRandom, ESexo.Macho, new DateTime(rnd.Next(2010, 2023), rnd.Next(1, 13), rnd.Next(1, 27)), rnd.Next(1000), rnd.Next(500), rnd.Next(100, 1000), rnd.Next(2).Equals(1), rnd.Next(2, 20));
                Vacuna randVacuna = Vacunas[rnd.Next(0, 5)];
                o.Vacunar(randVacuna, new DateTime(rnd.Next(o.FechaNacimiento.Year, 2023), rnd.Next(o.FechaNacimiento.Month, 13), rnd.Next(o.FechaNacimiento.Day, 27)));
                AltaOvino(o);

                Potrero potreroRandom = Potreros[rnd.Next(Potreros.Count)];
                potreroRandom.AgregarAnimal(o); // no se controla la excepcion porque todos los potreros default tienen capacidad suficiente
            }


            for (int i = 0; i < 30; i++)
            {
                // https://www.ganaderia.com/razas
                List<string> razas = ["Hereford", "Romagnola", "Gyr", "Brangus", "Angus", "Charolais"];

                string razaRandom = razas[rnd.Next(razas.Count)];

                // patron: [R]NNNNNNN
                // nota: probabilidad de colision!
                string idRandom = $"{razaRandom.ElementAt(0)}{rnd.Next(10)}{rnd.Next(10)}{rnd.Next(10)}{rnd.Next(10)}{rnd.Next(10)}{rnd.Next(10)}{rnd.Next(10)}";
                Bovino b = new Bovino(idRandom, razaRandom, ESexo.Macho, new DateTime(rnd.Next(2010, 2023), rnd.Next(1, 13), rnd.Next(1, 27)), rnd.Next(1000), rnd.Next(500), rnd.Next(100, 1000), rnd.Next(2).Equals(1), rnd.Next(2).Equals(1) ? EAlimentacion.Grano : EAlimentacion.Pastura);
                Vacuna randVacuna = Vacunas[rnd.Next(5, 10)];
                b.Vacunar(randVacuna, new DateTime(rnd.Next(b.FechaNacimiento.Year, 2023), rnd.Next(b.FechaNacimiento.Month, 13), rnd.Next(b.FechaNacimiento.Day, 27)));
                AltaBovino(b);

                Potrero potreroRandom = Potreros[rnd.Next(Potreros.Count)];
                potreroRandom.AgregarAnimal(b); // no se controla la excepcion porque todos los potreros default tienen capacidad suficiente
            }
        }
        catch (Exception e)
        {
            // debido al uso de ids pseudo-aleatorios, es posible que se generen colisiones!
            // falla en silencio y reintenta la recarga
            // warning: esto podria generar un loop infinito si hay errores en otra cosa
            // y por eso se usa la variable de control `ciclos`, la cual eventualmente
            // forzara a que se lance la excepcion
            if (ciclos < 5)
            {
                Precarga(ciclos + 1);
            }
            else
            {
                throw e;
            }
        }
    }
}
