# Komentář k řešení úkolu

### Shrnutí pracovního postupu
Jako první krok jsem se seznámila s kostrou řešení a prošla si jednotlivé odkazy ze zadání. Nejvíce jsem se zaměřila na vzorové vypracování OData Klienta - před samotným psaním kódu jsem se chtěla seznámit s tím, jak spolu jednotlivé podprojekty zhruba interagují. 

Následně jsem si zkusila načíst z demo aplikace malou část dat, a na ní provést základní operace (z asi 10 položek vypočítat podíl hodnot začínajících číslicí 1). Toto pokusné řešení jsem pak postupně rozšiřovala - počet hodnot začínajících jednotlivými číslicemi jsem se rozhodla uchovávat ve speciální entitě, nad kterou pak lze provádět další operace.

Na chvíli jsem se zastavila u volby nejvhodnějšího způsobu určení první číslice hodnoty, již při otestování na malém počtu položek se totiž moje pokusné řešení ukázalo jako nevhodné, jelikož trvalo nepřijatelně dlouho. Vzhledem k obvyklé velikosti hodnot atributu LineAmount jsem se nakonec rozhodla pro kombinovaný přístup - pro hodnoty v obvyklém rozmezí pomocí podmínek, a pro větší hodnoty pomocí převodu na string (vzhledem k jejich málo častému výskytu).
Vyšla jsem z informací na [tomto odkaze](https://stackoverflow.com/questions/701322/how-can-you-get-the-first-digit-in-an-int-c).

Dále jsem pak váhala ohledně toho, jakou metodou ověřit, že data odpovídají Benfordovu zákonu. Nakonec jsem se rozhodla implementovat dvě funkce:
- CompareBenford - vypíše očekávané zastoupení čísel začínajících danou číslicí, a jejich reálné naměřené zastoupení, funkce slouží primárně pro posouzení, že hodnoty "od oka" sedí/nesedí

- TestBenford - v této funkci jsem se pokusila implementovat statistický test dobré shody, vyšla jsem z [bakalářské práce](https://is.cuni.cz/webapps/zzp/detail/47310/) odkazované na wiki stránce Benfordova zákona

### Zhodnocení dosaženého výsledku
Řešení jsem byla schopna ověřit pouze na menším objemu dat, obecně trvá příliš dlouho. Domnívám se, že pro to, co jsem se pokusila implementovat, tedy budou existovat vhodnější a rychlejší řešení - pokusila jsem se je nalézt hlavně u zpracování načtených dat a samotného načítání, primárně pročtením Microsoft dokumentace k OData protokolu, a následně i podrobnější [Oasis dokumentace](http://docs.oasis-open.org/odata/odata/v4.0/errata03/os/complete/part1-protocol/odata-v4.0-errata03-os-part1-protocol-complete.pdf). Mým záměrem bylo, pokusit se zjistit, zda neexistuje způsob, jak počet prováděných operací nad mnoha tisíci načtených položek výrazně omezit, a zda část toho, co v kódu provádím, nelze provést již v připojené databázi.
