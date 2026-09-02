using System;
using System.Collections.Generic;

namespace H2_Lesson7
{
    /// <summary>
    /// En simpel, generisk samling, der kan indeholde elementer af én valgfri type.
    /// Typen bestemmes først, når samlingen oprettes, så den samme klasse kan bruges
    /// til både tekster, biler og alle andre typer uden casting.
    /// </summary>
    /// <typeparam name="T">Typen på de elementer, samlingen skal indeholde.</typeparam>
    public class Collection<T>
    {
        /// <summary>
        /// Den interne liste, som elementerne gemmes i. Den er privat, så elementerne
        /// kun kan ændres gennem samlingens egne metoder.
        /// </summary>
        private readonly List<T> _elements = new List<T>();

        /// <summary>
        /// Henter antallet af elementer i samlingen.
        /// </summary>
        /// <value>Antallet af elementer. Værdien er 0, når samlingen er tom.</value>
        public int Count => _elements.Count;

        /// <summary>
        /// Henter elementerne som en skrivebeskyttet liste, så de kan gennemløbes
        /// udefra uden at samlingens indhold kan ændres.
        /// </summary>
        /// <value>En skrivebeskyttet visning af elementerne i den rækkefølge, de blev tilføjet.</value>
        public IReadOnlyList<T> Items => _elements;

        /// <summary>
        /// Tilføjer et element til slutningen af samlingen. Samlingen tillader dubletter,
        /// så det samme element kan tilføjes flere gange.
        /// </summary>
        /// <param name="element">Elementet, der skal tilføjes til samlingen.</param>
        public void Add(T element)
        {
            _elements.Add(element);
        }

        /// <summary>
        /// Fjerner den første forekomst af et element fra samlingen, hvis elementet findes.
        /// Sammenligningen foretages med typens egen lighedssammenligning, dvs.
        /// <see cref="EqualityComparer{T}.Default"/>.
        /// </summary>
        /// <param name="element">Elementet, der skal fjernes fra samlingen.</param>
        /// <returns>
        /// <c>true</c>, hvis elementet blev fundet og fjernet, og <c>false</c>,
        /// hvis elementet ikke findes i samlingen.
        /// </returns>
        public bool Remove(T element)
        {
            return _elements.Remove(element);
        }

        /// <summary>
        /// Finder det første element i samlingen, der opfylder den angivne betingelse.
        /// Elementerne gennemløbes i den rækkefølge, de blev tilføjet, og søgningen
        /// stopper ved det første match.
        /// </summary>
        /// <param name="predicate">
        /// Betingelsen, som elementet skal opfylde. Betingelsen kaldes for ét element ad gangen
        /// og skal returnere <c>true</c> for det element, der søges efter.
        /// </param>
        /// <returns>
        /// Det første element, der opfylder betingelsen, eller <c>default(T)</c>,
        /// hvis intet element matcher. For referencetyper som <see cref="string"/>
        /// betyder det <c>null</c>, og for eksempelvis <see cref="int"/> betyder det 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Kastes, hvis <paramref name="predicate"/> er <c>null</c>.
        /// </exception>
        public T? Find(Func<T, bool> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate), "Der skal angives en betingelse at søge efter.");
            }

            foreach (T element in _elements)
            {
                if (predicate(element))
                {
                    return element;
                }
            }

            return default;
        }
    }
}
