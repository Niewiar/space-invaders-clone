# Space Invaders Clone

## 1. Opis projektu

**Space Invaders Clone** to klon klasycznej gry arcade *Space Invaders* stworzony przy użyciu silnika Unity. Projekt odtwarza podstawową mechanikę oryginału, w której:

- **Gracz** steruje statkiem kosmicznym umieszczonym na dole ekranu.
- **Przeciwnicy** pojawiają się w formacji, poruszają się poziomo, a po osiągnięciu krawędzi ekranu przesuwają się w dół. Dodatkowo formacja przyspiesza, gdy liczba żywych jednostek maleje.
- **Punktacja:** Po zniszczeniu przeciwników gracz otrzymuje punkty, a celem gry jest zdobycie jak największej ilości punktów oraz pobijanie własnych rekordów.

## 2. Struktura projektu

### 2.1 Maszyna stanów
Projekt jest sterowany za pomocą Finite State Machine zaimplementowanej w skrypcie `GameStateMachine.cs`. Maszyna stanów decyduje, jaki element gry jest aktualnie aktywny, na podstawie warunków określonych w następujących modułach:
- `Intro.cs`
- `Menu.cs`
- `GameplayController.cs`
- `GameEnd.cs`

### 2.2 Inicjalizacja gry
Zamiast korzystać z popularnego DI (Dependency Injection) za pomocą Zenject, projekt wykorzystuje statyczną klasę `GameInstance.cs`. Klasa ta:
- Przechowuje odwołania do głównych elementów gry.
- Jest inicjalizowana jako pierwsza ze wszystkich skryptów.
- Tworzy obiekt `GameStateMachine.cs`, który steruje przejściami między stanami gry.

### 2.3 GameplayController.cs
Ten skrypt przechowuje podstawowe dane dotyczące rozgrywki, takie jak liczba żyć gracza oraz jego wynik. Informuje również o zmianach interfejs `HUD.cs`, np. aktualizuje wyświetlaną punktację.

### 2.4 Elementy gry
- **Player.cs**  
  Klasa odpowiedzialna za sterowanie statkiem gracza. Informuje `GameplayController.cs` o zniszczeniu statku.
  
- **EnemiesPool.cs**  
  Pool (pulę) przeciwników ustawionych w rzędach. Zarządza poruszaniem się statków przeciwników oraz monitoruje ich zniszczenia.
  
- **UFO.cs**  
  Klasa dla dodatkowego przeciwnika, który pojawia się losowo i posiada zmienną bazę punktową.
  
- **EnemyBulletsPool.cs**  
  Pool pocisków wystrzeliwanych przez przeciwników.
  
- **Bullet.cs**  
  Klasa obsługująca zachowanie pocisków (ruch, kolizje).

### 2.5 Konfiguracja gry
Dane gry można modyfikować za pomocą pliku konfiguracyjnego `GameConfig.json`, który jest tworzony w folderze:
...\AppData\LocalLow\Niewiar\Space Invaders

Dodatkowo, tekstury przeciwników można podmieniać w folderze:
...\Space Invaders_Data\StreamingAssets

> **Uwaga:** Po podmianie tekstur należy również zaktualizować nazwy plików w pliku konfiguracyjnym, a także upewnić się, że rozmiary plików są zgodne z plikem konfiguracyjnym.

## 3. Mechanika rozgrywki

### 3.1 Ruch i atak
- **Gracz:**  
  Steruje statkiem znajdującym się na dole ekranu, poruszając się w poziomie. Może wystrzeliwać pocisk `Bullet.cs`, który jest oddzielną i jedyną instancją na scenie przypisaną do obiektu gracza.
  
- **Przeciwnicy:**  
  Pojawiają się w formacji, poruszają się poziomo, a po osiągnięciu krawędzi ekranu przesuwają się w dół. Mechanizm przyspieszania przeciwników zwiększa trudność w miarę niszczenia kolejnych jednostek.

### 3.2 Punktacja i stany gry
- **Punktacja:**  
  Gracz zdobywa punkty za zestrzelenie przeciwników.
  
- **Życia i kończenie gry:**  
  Gracz rozpoczyna rozgrywkę z określoną liczbą żyć. Po utracie wszystkich żyć lub w momencie, gdy przeciwnicy dotrą do strefy gracza, wyświetlany jest ekran „Game End” z możliwością restartu gry.

## 4. Podsumowanie

Projekt **Space Invaders Clone** powstał, aby pokazać umiejętności kodowania oraz znajomość silnika Unity. Projekt jest zaprojektowany w sposób umożliwiający łatwe rozszerzanie i modyfikację konfiguracji gry. W przyszłości planowane są:
- Dodanie edytorów ułatwiających pracę bezpośrednio w Unity.
- Rozszerzenie mechanik gry, np. o dodatkowe typy przeciwników, ulepszenia statku czy zaawansowane systemy punktacji.

Projekt stanowi solidną bazę do dalszego rozwoju oraz nauki programowania gier w Unity.

---
