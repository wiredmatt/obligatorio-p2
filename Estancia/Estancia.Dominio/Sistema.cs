namespace Estancia.Dominio;

public class Sistema
{
    private static Sistema instancia;

    private List<Empleado> Empleados { get; set; } = new List<Empleado>();
    private List<Animal> Animales { get; set; } = new List<Animal>();
    private List<Potrero> Potreros { get; set; } = new List<Potrero>();
    private List<Vacuna> Vacunas { get; set; } = new List<Vacuna>();

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

    public IEnumerable<Potrero> GetPotreros()
    {
        Potreros.Sort();
        return Potreros;
    }

    public IEnumerable<Empleado> GetEmpleados() { return Empleados; }
    public IEnumerable<Vacuna> GetVacunas() { return Vacunas; }
    public IEnumerable<Animal> GetAnimales() { return Animales; }

    public Animal? GetAnimalPorID(string ID)
    {
        foreach (Animal a in Animales) if (a.ID == ID) return a;
        return null;
    }

    public Vacuna? GetVacunaPorNombre(string nombre)
    {
        foreach (Vacuna v in Vacunas) if (v.Nombre == nombre) return v;
        return null;
    }

    public IEnumerable<Peon> GetPeones()
    {
        List<Peon> peones = new List<Peon>();

        foreach (Empleado e in Empleados)
        {
            if (e.GetTipo() == "Peon")
            {
                Peon peon = e as Peon;
                peones.Add(peon);
            }
        }

        return peones;
    }

    public Peon? GetPeonPorID(int ID)
    {
        IEnumerable<Peon> peones = GetPeones();

        foreach (Peon p in peones)
        {
            if (p.ID == ID)
            {
                return p;
            }
        }

        return null;
    }

    public IEnumerable<Capataz> GetCapataces()
    {
        List<Capataz> capataces = new List<Capataz>();

        foreach (Empleado e in Empleados)
        {
            if (e.GetTipo() == "Capataz")
            {
                Capataz c = e as Capataz;
                capataces.Add(c);
            }
        }

        return capataces;
    }

    public Capataz? GetCapatazPorID(int ID)
    {
        IEnumerable<Capataz> capataces = GetCapataces();

        foreach (Capataz c in capataces)
        {
            if (c.ID == ID)
            {
                return c;
            }
        }

        return null;
    }

    public void AsignarTareaAPeon(Tarea t, int idPeon)
    {
        Peon? p = GetPeonPorID(idPeon) ?? throw new ErrorDeValidacion("No existe un Peon con ese ID");
        p.AltaTarea(t);
    }

    public Empleado? GetEmpleadoPorEmail(string email)
    {
        foreach (Empleado e in Empleados)
        {
            if (e.Mail == email) return e;
        }

        return null;
    }

    public Empleado? Login(string email, string contrasena)
    {
        Empleado? e = GetEmpleadoPorEmail(email);

        if (e != null && e.Contrasena == contrasena) return e;

        throw new ErrorDeValidacion("Credenciales incorrectas.");
    }

    public Peon? RegistroPeon(string mail, string contrasena, string nombre, bool esResidente)
    {
        Peon p = new Peon(mail, contrasena, nombre, DateTime.Now, esResidente);
        p.Validar();

        Empleado yaExistente = GetEmpleadoPorEmail(mail);

        if (yaExistente != null)
        {
            throw new ErrorDeValidacion("Ya existe un empleado registrado con ese mail");
        }

        Empleados.Add(p);

        return p;
    }

    public void AltaEmpleado(Empleado e)
    {
        e.Validar();

        Empleado? empleadoYaExistente = GetEmpleadoPorEmail(e.Mail);

        if (empleadoYaExistente != null)
        {
            throw new ErrorDeValidacion("Ya existe un empleado con ese mail.");
        }

        Empleados.Add(e);
    }

    public void AltaAnimal(Animal animal)
    {
        animal.Validar();

        Animal? animalYaExistente = GetAnimalPorID(animal.ID);

        if (animalYaExistente != null)
        {
            throw new ErrorDeValidacion("Ya existe un animal con esa caravana");
        }

        Animales.Add(animal);
    }

    public IEnumerable<Animal> GetAnimalesLibres()
    {
        List<Animal> animales = new List<Animal>();

        foreach (Animal a in Animales)
        {
            if (a.EstaLibre)
            {
                animales.Add(a);
            }
        }

        return animales;
    }

    public IEnumerable<Animal> GetAnimalesPorTipoYPeso(string tipo, double peso)
    {
        List<Animal> animales = new List<Animal>();

        foreach (Animal a in Animales)
        {
            if (a.GetTipo() == tipo && a.Peso > peso)
            {
                animales.Add(a);
            }
        }

        return animales;
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

    public IEnumerable<Potrero> GetPotreros(int hectareas, int capacidad)
    {
        List<Potrero> potreros = new List<Potrero>();

        foreach (Potrero p in Potreros)
        {
            if (p.Hectareas >= hectareas && p.Capacidad >= capacidad)
            {
                potreros.Add(p);
            }
        }

        potreros.Sort();

        return potreros;
    }

    public Potrero? GetPotreroPorID(int ID)
    {
        foreach (Potrero p in Potreros) if (p.ID == ID) return p;
        return null;
    }

    public static void EstablecerPrecioLana(double precio)
    {
        Ovino.PrecioKgLana = precio;
    }

    private void Precarga()
    {
        #region Capataces
        Capataz capataz1 = new Capataz("oscar@estancia.com", "12345678", "Oscar", new DateTime(2018, 7, 20), 5);
        Capataz capataz2 = new Capataz("ana@estancia.com", "12345678", "Ana", new DateTime(2019, 11, 15), 8);
        AltaEmpleado(capataz1);
        AltaEmpleado(capataz2);
        #endregion

        #region Peones
        Peon peon1 = new Peon("laura@estancia.com", "12345678", "Laura", new DateTime(2019, 5, 10), false);
        Peon peon2 = new Peon("juan@estancia.com", "12345678", "Juan", new DateTime(2021, 8, 15), true);
        Peon peon3 = new Peon("marcela@estancia.com", "12345678", "Marcela", new DateTime(2020, 11, 20), false);
        Peon peon4 = new Peon("carlos@estancia.com", "12345678", "Carlos", new DateTime(2022, 1, 5), true);
        Peon peon5 = new Peon("maria@estancia.com", "12345678", "Maria", new DateTime(2021, 3, 25), false);
        Peon peon6 = new Peon("pedro@estancia.com", "12345678", "Pedro", new DateTime(2019, 9, 12), true);
        Peon peon7 = new Peon("julia@estancia.com", "12345678", "Julia", new DateTime(2020, 6, 30), false);
        Peon peon8 = new Peon("lucas@estancia.com", "12345678", "Lucas", new DateTime(2022, 2, 8), true);
        Peon peon9 = new Peon("silvia@estancia.com", "12345678", "Silvia", new DateTime(2019, 10, 17), false);
        Peon peon10 = new Peon("marcelo@estancia.com", "12345678", "Marcelo", new DateTime(2021, 12, 3), true);
        List<Peon> peones = new List<Peon> { peon1, peon2, peon3, peon4, peon5, peon6, peon7, peon8, peon9, peon10 };
        foreach (Peon p in peones)
        {
            AltaEmpleado(p);

            #region Tareas
            Tarea tarea1 = new Tarea("Cosecha de cultivos", new DateTime(2024, 4, 1), new DateTime(2024, 4, 15), capataz1);
            Tarea tarea2 = new Tarea("Reparación de cercas", new DateTime(2024, 3, 25), new DateTime(2024, 4, 10), capataz1);
            Tarea tarea3 = new Tarea("Limpieza de establos", new DateTime(2024, 4, 5), new DateTime(2024, 4, 20), capataz2);
            Tarea tarea4 = new Tarea("Siembra de semillas", new DateTime(2024, 4, 10), new DateTime(2024, 4, 25), capataz2);
            Tarea tarea5 = new Tarea("Alimentación del ganado", new DateTime(2024, 4, 8), new DateTime(2024, 4, 22), capataz1);
            Tarea tarea6 = new Tarea("Podado de árboles", new DateTime(2024, 4, 12), new DateTime(2024, 4, 27), capataz2);
            Tarea tarea7 = new Tarea("Recolección de huevos", new DateTime(2024, 4, 7), new DateTime(2024, 4, 21), capataz1);
            Tarea tarea8 = new Tarea("Control de plagas", new DateTime(2024, 4, 3), new DateTime(2024, 4, 18), capataz2);
            Tarea tarea9 = new Tarea("Mantenimiento de maquinaria", new DateTime(2024, 4, 15), new DateTime(2024, 4, 30), capataz1);
            Tarea tarea10 = new Tarea("Recogida de frutos", new DateTime(2024, 4, 2), new DateTime(2024, 4, 17), capataz2);
            Tarea tarea11 = new Tarea("Riego de campos", new DateTime(2024, 4, 6), new DateTime(2024, 4, 21), capataz1);
            Tarea tarea12 = new Tarea("Cuidado de crías", new DateTime(2024, 4, 9), new DateTime(2024, 4, 24), capataz2);
            Tarea tarea13 = new Tarea("Reparación de herramientas", new DateTime(2024, 4, 13), new DateTime(2024, 4, 28), capataz1);
            Tarea tarea14 = new Tarea("Fertilización de tierras", new DateTime(2024, 4, 4), new DateTime(2024, 4, 19), capataz2);
            Tarea tarea15 = new Tarea("Ordenamiento de almacenes", new DateTime(2024, 4, 11), new DateTime(2024, 4, 26), capataz1);

            List<Tarea> tareas = new List<Tarea> { tarea1, tarea2, tarea3, tarea4, tarea5, tarea6, tarea7, tarea8, tarea9, tarea10, tarea11, tarea12, tarea13, tarea14, tarea15 };

            foreach (Tarea t in tareas)
            {
                p.AltaTarea(t);

                if (t.ID % 2 == 0)
                {
                    t.Completar("Todo bien!"); // las tareas 0, 2, 4, 6, 8, 10, 12, 14 van a estar marcadas como Completadas.
                }
            }
            #endregion
        }
        #endregion

        #region Potreros
        Potrero potrero1 = new Potrero("Potrero principal", 50, 100);
        Potrero potrero2 = new Potrero("Potrero norte", 40, 80);
        Potrero potrero3 = new Potrero("Potrero sur", 35, 70);
        Potrero potrero4 = new Potrero("Potrero este", 45, 90);
        Potrero potrero5 = new Potrero("Potrero oeste", 55, 110);
        Potrero potrero6 = new Potrero("Potrero A", 30, 60);
        Potrero potrero7 = new Potrero("Potrero B", 25, 50);
        Potrero potrero8 = new Potrero("Potrero C", 60, 120);
        Potrero potrero9 = new Potrero("Potrero D", 20, 40);
        Potrero potrero10 = new Potrero("Potrero E", 15, 30);
        List<Potrero> potreros = new List<Potrero> { potrero1, potrero2, potrero3, potrero4, potrero5, potrero6, potrero7, potrero8, potrero9, potrero10 };
        foreach (Potrero p in potreros)
        {
            AltaPotrero(p);
        }
        #endregion

        #region Vacunas
        Vacuna vacunaOvino1 = new Vacuna("Vacuna Ovino A", "Protección contra enfermedades comunes en ovinos", "Patógeno X");
        Vacuna vacunaOvino2 = new Vacuna("Vacuna Ovino B", "Prevención de infecciones en ovinos", "Patógeno Y");
        Vacuna vacunaOvino3 = new Vacuna("Vacuna Ovino C", "Inmunización contra enfermedades virales en ovinos", "Patógeno Z");
        Vacuna vacunaOvino4 = new Vacuna("Vacuna Ovino D", "Protección contra parásitos internos en ovinos", "Patógeno W");
        Vacuna vacunaOvino5 = new Vacuna("Vacuna Ovino E", "Prevención de afecciones respiratorias en ovinos", "Patógeno V");
        List<Vacuna> vacunasOvinos = new List<Vacuna> { vacunaOvino1, vacunaOvino2, vacunaOvino3, vacunaOvino4, vacunaOvino5 };
        foreach (Vacuna v in vacunasOvinos)
        {
            AltaVacuna(v);
        }

        Vacuna vacunaBovino1 = new Vacuna("Vacuna Bovino A", "Protección contra enfermedades gastrointestinales en bovinos", "Patógeno M");
        Vacuna vacunaBovino2 = new Vacuna("Vacuna Bovino B", "Prevención de infecciones en bovinos", "Patógeno N");
        Vacuna vacunaBovino3 = new Vacuna("Vacuna Bovino C", "Inmunización contra enfermedades virales en bovinos", "Patógeno O");
        Vacuna vacunaBovino4 = new Vacuna("Vacuna Bovino D", "Protección contra parásitos externos en bovinos", "Patógeno P");
        Vacuna vacunaBovino5 = new Vacuna("Vacuna Bovino E", "Prevención de afecciones respiratorias en bovinos", "Patógeno Q");
        List<Vacuna> vacunasBovinos = new List<Vacuna> { vacunaBovino1, vacunaBovino2, vacunaBovino3, vacunaBovino4, vacunaBovino5 };
        foreach (Vacuna v in vacunasBovinos)
        {
            AltaVacuna(v);
        }
        #endregion

        #region Ovinos
        Ovino ovino1 = new Ovino("O01MA001", "Merino", ESexo.Macho, new DateTime(2021, 3, 15), 500, 200, 50, false, 3);
        Ovino ovino2 = new Ovino("O02CR002", "Corriedale", ESexo.Hembra, new DateTime(2020, 5, 20), 600, 250, 55, false, 4);
        Ovino ovino3 = new Ovino("O03DU001", "Dorper", ESexo.Macho, new DateTime(2019, 9, 10), 550, 220, 52, false, 3.5);
        Ovino ovino4 = new Ovino("O04SO002", "Suffolk", ESexo.Hembra, new DateTime(2022, 1, 5), 650, 280, 60, false, 4.5);
        Ovino ovino5 = new Ovino("O05CO001", "Corriedale", ESexo.Macho, new DateTime(2020, 8, 25), 500, 200, 50, false, 3);
        Ovino ovino6 = new Ovino("O06ME002", "Merino", ESexo.Hembra, new DateTime(2019, 10, 30), 600, 250, 55, false, 4);
        Ovino ovino7 = new Ovino("O07DO001", "Dorper", ESexo.Macho, new DateTime(2018, 12, 12), 550, 220, 52, false, 3.5);
        Ovino ovino8 = new Ovino("O08SU002", "Suffolk", ESexo.Hembra, new DateTime(2022, 4, 2), 650, 280, 60, false, 4.5);
        Ovino ovino9 = new Ovino("O09MA001", "Merino", ESexo.Macho, new DateTime(2021, 3, 15), 500, 200, 50, false, 3);
        Ovino ovino10 = new Ovino("O10CR002", "Corriedale", ESexo.Hembra, new DateTime(2020, 5, 20), 600, 250, 55, false, 4);
        Ovino ovino11 = new Ovino("O11DU001", "Dorper", ESexo.Macho, new DateTime(2019, 9, 10), 550, 220, 52, false, 3.5);
        Ovino ovino12 = new Ovino("O12SO002", "Suffolk", ESexo.Hembra, new DateTime(2022, 1, 5), 650, 280, 60, false, 4.5);
        Ovino ovino13 = new Ovino("O13CO001", "Corriedale", ESexo.Macho, new DateTime(2020, 8, 25), 500, 200, 50, false, 3);
        Ovino ovino14 = new Ovino("O14ME002", "Merino", ESexo.Hembra, new DateTime(2019, 10, 30), 600, 250, 55, false, 4);
        Ovino ovino15 = new Ovino("O15DO001", "Dorper", ESexo.Macho, new DateTime(2018, 12, 12), 550, 220, 52, false, 3.5);
        Ovino ovino16 = new Ovino("O16SU002", "Suffolk", ESexo.Hembra, new DateTime(2022, 4, 2), 650, 280, 60, false, 4.5);
        Ovino ovino17 = new Ovino("O17MA001", "Merino", ESexo.Macho, new DateTime(2021, 3, 15), 500, 200, 50, false, 3);
        Ovino ovino18 = new Ovino("O18CR002", "Corriedale", ESexo.Hembra, new DateTime(2020, 5, 20), 600, 250, 55, false, 4);
        Ovino ovino19 = new Ovino("O19DU001", "Dorper", ESexo.Macho, new DateTime(2019, 9, 10), 550, 220, 52, false, 3.5);
        Ovino ovino20 = new Ovino("O20SO002", "Suffolk", ESexo.Hembra, new DateTime(2022, 1, 5), 650, 280, 60, false, 4.5);
        Ovino ovino21 = new Ovino("O21CO001", "Corriedale", ESexo.Macho, new DateTime(2020, 8, 25), 500, 200, 50, false, 3);
        Ovino ovino22 = new Ovino("O22ME002", "Merino", ESexo.Hembra, new DateTime(2019, 10, 30), 600, 250, 55, false, 4);
        Ovino ovino23 = new Ovino("O23DO001", "Dorper", ESexo.Macho, new DateTime(2018, 12, 12), 550, 220, 52, false, 3.5);
        Ovino ovino24 = new Ovino("O24SU002", "Suffolk", ESexo.Hembra, new DateTime(2022, 4, 2), 650, 280, 60, false, 4.5);
        Ovino ovino25 = new Ovino("O25MA001", "Merino", ESexo.Macho, new DateTime(2021, 3, 15), 500, 200, 50, false, 3);
        Ovino ovino26 = new Ovino("O26CR002", "Corriedale", ESexo.Hembra, new DateTime(2020, 5, 20), 600, 250, 55, false, 4);
        Ovino ovino27 = new Ovino("O27DU001", "Dorper", ESexo.Macho, new DateTime(2019, 9, 10), 550, 220, 52, false, 3.5);
        Ovino ovino28 = new Ovino("O28SO002", "Suffolk", ESexo.Hembra, new DateTime(2022, 1, 5), 650, 280, 60, false, 4.5);
        Ovino ovino29 = new Ovino("O29CO001", "Corriedale", ESexo.Macho, new DateTime(2020, 8, 25), 500, 200, 50, false, 3);
        Ovino ovino30 = new Ovino("O30ME002", "Merino", ESexo.Hembra, new DateTime(2019, 10, 30), 600, 250, 55, false, 4);
        List<Ovino> ovinos = new List<Ovino> { ovino1, ovino2, ovino3, ovino4, ovino5, ovino6, ovino7, ovino8, ovino9, ovino10,
                                                      ovino11, ovino12, ovino13, ovino14, ovino15, ovino16, ovino17, ovino18, ovino19, ovino20,
                                                      ovino21, ovino22, ovino23, ovino24, ovino25, ovino26, ovino27, ovino28, ovino29, ovino30 };
        foreach (Ovino o in ovinos)
        {
            o.Vacunar(vacunasOvinos[0], new DateTime(2023, 10, 10));
            AltaAnimal(o);

            if (o.ID.EndsWith('1'))
            {
                potrero1.AgregarAnimal(o);
            }
            else
            {
                potrero2.AgregarAnimal(o);
            }
        }
        #endregion


        #region Bovinos
        Bovino bovino1 = new Bovino("B01AB001", "Angus", ESexo.Macho, new DateTime(2021, 3, 15), 1500, 500, 300, false, EAlimentacion.Grano);
        Bovino bovino2 = new Bovino("B02HR002", "Hereford", ESexo.Hembra, new DateTime(2020, 5, 20), 1600, 550, 320, false, EAlimentacion.Pastura);
        Bovino bovino3 = new Bovino("B03BB001", "Brahman", ESexo.Macho, new DateTime(2019, 9, 10), 1400, 480, 280, false, EAlimentacion.Grano);
        Bovino bovino4 = new Bovino("B04BD002", "Braford", ESexo.Hembra, new DateTime(2022, 1, 5), 1550, 520, 310, false, EAlimentacion.Pastura);
        Bovino bovino5 = new Bovino("B05HL001", "Holstein", ESexo.Macho, new DateTime(2020, 8, 25), 1450, 490, 290, false, EAlimentacion.Grano);
        Bovino bovino6 = new Bovino("B06SM002", "Simmental", ESexo.Hembra, new DateTime(2019, 10, 30), 1650, 560, 330, false, EAlimentacion.Pastura);
        Bovino bovino7 = new Bovino("B07CH001", "Charolais", ESexo.Macho, new DateTime(2018, 12, 12), 1350, 470, 270, false, EAlimentacion.Grano);
        Bovino bovino8 = new Bovino("B08LM002", "Limousin", ESexo.Hembra, new DateTime(2022, 4, 2), 1700, 580, 340, false, EAlimentacion.Pastura);
        Bovino bovino9 = new Bovino("B09GR001", "Gyr", ESexo.Macho, new DateTime(2020, 2, 18), 1550, 520, 310, false, EAlimentacion.Grano);
        Bovino bovino10 = new Bovino("B10JS002", "Jersey", ESexo.Hembra, new DateTime(2019, 6, 8), 1600, 550, 320, false, EAlimentacion.Pastura);
        Bovino bovino11 = new Bovino("B11AB001", "Angus", ESexo.Macho, new DateTime(2021, 3, 15), 1500, 500, 300, false, EAlimentacion.Grano);
        Bovino bovino12 = new Bovino("B12HR002", "Hereford", ESexo.Hembra, new DateTime(2020, 5, 20), 1600, 550, 320, false, EAlimentacion.Pastura);
        Bovino bovino13 = new Bovino("B13BB001", "Brahman", ESexo.Macho, new DateTime(2019, 9, 10), 1400, 480, 280, false, EAlimentacion.Grano);
        Bovino bovino14 = new Bovino("B14BD002", "Braford", ESexo.Hembra, new DateTime(2022, 1, 5), 1550, 520, 310, false, EAlimentacion.Pastura);
        Bovino bovino15 = new Bovino("B15HL001", "Holstein", ESexo.Macho, new DateTime(2020, 8, 25), 1450, 490, 290, false, EAlimentacion.Grano);
        Bovino bovino16 = new Bovino("B16SM002", "Simmental", ESexo.Hembra, new DateTime(2019, 10, 30), 1650, 560, 330, false, EAlimentacion.Pastura);
        Bovino bovino17 = new Bovino("B17CH001", "Charolais", ESexo.Macho, new DateTime(2018, 12, 12), 1350, 470, 270, false, EAlimentacion.Grano);
        Bovino bovino18 = new Bovino("B18LM002", "Limousin", ESexo.Hembra, new DateTime(2022, 4, 2), 1700, 580, 340, false, EAlimentacion.Pastura);
        Bovino bovino19 = new Bovino("B19GR001", "Gyr", ESexo.Macho, new DateTime(2020, 2, 18), 1550, 520, 310, false, EAlimentacion.Grano);
        Bovino bovino20 = new Bovino("B20JS002", "Jersey", ESexo.Hembra, new DateTime(2019, 6, 8), 1600, 550, 320, false, EAlimentacion.Pastura);
        Bovino bovino21 = new Bovino("B21AB001", "Angus", ESexo.Macho, new DateTime(2021, 3, 15), 1500, 500, 300, false, EAlimentacion.Grano);
        Bovino bovino22 = new Bovino("B22HR002", "Hereford", ESexo.Hembra, new DateTime(2020, 5, 20), 1600, 550, 320, false, EAlimentacion.Pastura);
        Bovino bovino23 = new Bovino("B23BB001", "Brahman", ESexo.Macho, new DateTime(2019, 9, 10), 1400, 480, 280, false, EAlimentacion.Grano);
        Bovino bovino24 = new Bovino("B24BD002", "Braford", ESexo.Hembra, new DateTime(2022, 1, 5), 1550, 520, 310, false, EAlimentacion.Pastura);
        Bovino bovino25 = new Bovino("B25HL001", "Holstein", ESexo.Macho, new DateTime(2020, 8, 25), 1450, 490, 290, false, EAlimentacion.Grano);
        Bovino bovino26 = new Bovino("B26SM002", "Simmental", ESexo.Hembra, new DateTime(2019, 10, 30), 1650, 560, 330, false, EAlimentacion.Pastura);
        Bovino bovino27 = new Bovino("B27CH001", "Charolais", ESexo.Macho, new DateTime(2018, 12, 12), 1350, 470, 270, false, EAlimentacion.Grano);
        Bovino bovino28 = new Bovino("B28LM002", "Limousin", ESexo.Hembra, new DateTime(2022, 4, 2), 1700, 580, 340, false, EAlimentacion.Pastura);
        Bovino bovino29 = new Bovino("B29GR001", "Gyr", ESexo.Macho, new DateTime(2020, 2, 18), 1550, 520, 310, false, EAlimentacion.Grano);
        Bovino bovino30 = new Bovino("B30JS002", "Jersey", ESexo.Hembra, new DateTime(2019, 6, 8), 1600, 550, 320, false, EAlimentacion.Pastura);


        List<Bovino> bovinos = new List<Bovino> { bovino1, bovino2, bovino3, bovino4, bovino5, bovino6, bovino7, bovino8, bovino9, bovino10,
                                                      bovino11, bovino12, bovino13, bovino14, bovino15, bovino16, bovino17, bovino18, bovino19, bovino20,
                                                      bovino21, bovino22, bovino23, bovino24, bovino25, bovino26, bovino27, bovino28, bovino29, bovino30 };

        foreach (Bovino b in bovinos)
        {
            b.Vacunar(vacunasBovinos[0], new DateTime(2023, 10, 10));
            AltaAnimal(b);

            if (b.ID.EndsWith('1'))
            {
                potrero3.AgregarAnimal(b);
            }
            else
            {
                potrero4.AgregarAnimal(b);
            }
        }

        Bovino bovino31 = new Bovino("BL3LS001", "Holstein", ESexo.Hembra, new DateTime(2021, 5, 10), 1550, 520, 410, true, EAlimentacion.Pastura);
        Bovino bovino32 = new Bovino("BL3LS002", "Simmental", ESexo.Macho, new DateTime(2020, 2, 3), 1600, 550, 300, true, EAlimentacion.Grano);

        AltaAnimal(bovino31);
        AltaAnimal(bovino32);
        #endregion
    }

}
