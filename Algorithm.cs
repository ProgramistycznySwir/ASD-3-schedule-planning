using System;
using System.Collections.Generic;


namespace ASD___3
{
    // Rozwiązanie ma pesymistyczną złożoność czasową: O(n)=n.
    // Złożoność średnia dominowana jest głównie przez funkcję Group_LectureEnd() która zawsze ma złożoność n.
    // Bez niej optymistyczna złożoność czasowa rozwiązania jest równa log_k(n) (nie licząc czasu na poszerzanie listy i iterację
    //  który jest marginalny).
    public static class Algorithm
    {
        public static int Solution(List<Lecture> lectures, int k)
        {
            // Count+Bucket Sort
            // Dla szybkości algorytmu można nie dość że posortować to zgrupować ze sobą wykłady o tym samym czasie rozpoczęcia.
            List<Stack<Lecture>> buckets = Group_LectureEnd(lectures, k);

            int totalLectureTime = 0;

            var bestChoiceTime = new List<int>(k + 1);
            bestChoiceTime.Add(0);

            // Funkcja iteruje przez cały zbiór bucketów.
            for (int i = 1; i <= k; i++) // i = 12
            {
                bestChoiceTime.Add(bestChoiceTime[i - 1]);
                // Dla każdego bucketa sprawdza czy nie znajduje się w nim lepsze czasowo rozwiązanie.
                foreach(Lecture lecture in buckets[i])
                    bestChoiceTime[i] = Math.Max(bestChoiceTime[i], bestChoiceTime[lecture.start] + i - lecture.start);
            }

            // Na końcu listy bestChoiceTime znajduje się nasze rozwiązanie
            return bestChoiceTime[k];
        }

        // Funkcja ta działa podobnie do bucket sorta z tą różnicą że bucketów jest tyle co zakres danych i rzeczy w
        //  bucket'cie nie są dalej sortowane.
        public static List<Stack<Lecture>> Group_LectureEnd(List<Lecture> list, int k)
        {
            var buckets = new List<Stack<Lecture>>(k+1);

            for (int i = 0; i <= k; i++)
                buckets.Add(new Stack<Lecture>());

            for (int i = 0; i < list.Count; i++)
                buckets[list[i].end].Push(list[i]);

            return buckets;
        }
    }
}
/*
 To sortowanie jakie zastosowałem prawdopodobnie nazywa się inaczej, jednak mi kojarzy się z okrojonym Bucket/Count sortem,
  a z racji, że zadanie wymagało znalezienia najoptymalniejszego algorytmu, a nie odpowiedniej nomenklatury, pozwolę sobie
  nie korygować tego błędu :)
*/

/// EDIT:
/*
 Można to zoptymalizować dalej poprzez przechowywanie w grupach tylko godziny rozpoczęcia wykładu, bo godzina zakończenia
  to indeks grupy, jednak to jest prosta optymalizacja, a nie chcę zmieniać kodu już dodanego na cez.
*/