import asyncio
import dataclasses
import typing
import random
from pprint import pprint

import aiohttp


@dataclasses.dataclass
class Organizer:
    title: str
    mail: str
    content: str
    guid: typing.Optional[str] = None


organizers = (
    # Россия
    Organizer(title='Gaijin Entertainment', mail='gaijin@mail.com', content='Разработчики War Thunder, Crossout'),
    Organizer(title='1C Company', mail='1c@mail.com', content='Издатели IL-2 Sturmovik, King\'s Bounty'),
    Organizer(title='Saber Interactive', mail='saber@mail.com', content='World War Z, SnowRunner'),
    Organizer(title='Леста', mail='lesta@mail.com', content='World of Tanks Blitz (ранее)'),
    Organizer(title='Battlestate Games', mail='battlestate@mail.com', content='Escape from Tarkov'),
    Organizer(title='Ice-Pick Lodge', mail='icepick@mail.com', content='Pathologic, The Void'),
    Organizer(title='Mundfish', mail='mundfish@mail.com', content='Atomic Heart'),
    Organizer(title='Katauri Interactive', mail='katauri@mail.com', content='Space Rangers, Royal Quest'),
    Organizer(title='Allods Team', mail='allods@mail.com', content='Allods Online, Skyforge'),
    Organizer(title='Nival', mail='nival@mail.com', content='Blitzkrieg, Prime World'),
    Organizer(title='Cyberia Nova', mail='cyberia@mail.com', content='CONSCRIPT (retro survival horror)'),
    
    # Беларусь
    Organizer(title='Belka Games', mail='belka@mail.com', content='Clockmaker, Seekers Notes'),
    Organizer(title='Melesta Games', mail='melesta@mail.com', content='Perimeter, The Wild Eight'),
    
    # Казахстан
    Organizer(title='Weappy Studio', mail='weappy@mail.com', content='This Is the Police'),
    Organizer(title='Satur Entertainment', mail='satur@mail.com', content='The Perfect Tower'),
    
    # Другие страны СНГ
    Organizer(title='Azerion (Азербайджан)', mail='azerion@mail.com', content='Мобильные и браузерные игры'),
    Organizer(title='Somnia Labs (Армения)', mail='somnia@mail.com', content='VR-игры'),
)


@dataclasses.dataclass
class Event:
    title: str
    country: str
    region: str
    city: str
    address: str
    geo_url: str
    status: str
    price_min: int
    price_max: int
    organizer: Organizer


events = (
    # Gaijin Entertainment
    Event(
        title="Чемпионат мира по War Thunder 2024",
        country="Россия",
        region="Москва",
        city="Москва",
        address="Экспоцентр, павильон 7",
        geo_url="https://maps.google.com/expocenter-moscow",
        status="Предстоящее",
        price_min=0,
        price_max=5000,
        organizer=organizers[0]
    ),
    
    # 1C Company
    Event(
        title="Авиасимуляторная выставка IL-2 Штурмовик",
        country="Россия",
        region="Московская область",
        city="Жуковский",
        address="Лётно-исследовательский институт им. Громова",
        geo_url="https://maps.google.com/lisi",
        status="Идёт сейчас",
        price_min=1000,
        price_max=10000,
        organizer=organizers[1]
    ),
    
    # Saber Interactive
    Event(
        title="Экстремальные гонки SnowRunner",
        country="Россия",
        region="Красноярский край",
        city="Норильск",
        address="Полигон для испытания грузовиков",
        geo_url="https://maps.google.com/norilsk-trucks",
        status="Анонсировано",
        price_min=500,
        price_max=3000,
        organizer=organizers[2]
    ),
    
    # Battlestate Games
    Event(
        title="LAN-турнир по Escape from Tarkov",
        country="Россия",
        region="Санкт-Петербург",
        city="Санкт-Петербург",
        address="Сибур Арена, зал B",
        geo_url="https://maps.google.com/sibur-arena",
        status="Предстоящее",
        price_min=2000,
        price_max=15000,
        organizer=organizers[4]
    ),
    
    # Mundfish
    Event(
        title="Демонстрация технологий Atomic Heart",
        country="Россия",
        region="Москва",
        city="Москва",
        address="Центр Digital October",
        geo_url="https://maps.google.com/digital-october",
        status="Завершено",
        price_min=0,
        price_max=0,
        organizer=organizers[6]
    ),
    
    # Belka Games
    Event(
        title="Конференция мобильных разработчиков",
        country="Беларусь",
        region="Минск",
        city="Минск",
        address="Конференц-центр ПВТ",
        geo_url="https://maps.google.com/htp-minsk",
        status="Анонсировано",
        price_min=3000,
        price_max=25000,
        organizer=organizers[12]
    ),
    
    # Weappy Studio (Казахстан)
    Event(
        title="Лекция разработчиков This Is The Police",
        country="Казахстан",
        region="Алматы",
        city="Алматы",
        address="Технопарк Алматы",
        geo_url="https://maps.google.com/almaty-tech",
        status="Предстоящее",
        price_min=500,
        price_max=5000,
        organizer=organizers[14]
    ),
    
    # Azerion (Азербайджан)
    Event(
        title="Саммит разработчиков игр Кавказа",
        country="Азербайджан",
        region="Баку",
        city="Баку",
        address="Бакинский конгресс-центр",
        geo_url="https://maps.google.com/baku-congress",
        status="Анонсировано",
        price_min=2000,
        price_max=15000,
        organizer=organizers[16]
    ),
    
    # Дополнительные события
    Event(
        title="Фестиваль космических игр от Katauri",
        country="Россия",
        region="Новосибирская область",
        city="Новосибирск",
        address="Кванториум, ул. Николаева 12",
        geo_url="https://maps.google.com/kvantorium-nsk",
        status="Предстоящее",
        price_min=0,
        price_max=2000,
        organizer=organizers[7]
    ),
    
    Event(
        title="Хоррор-марафон от Cyberia Nova",
        country="Россия",
        region="Свердловская область",
        city="Екатеринбург",
        address="Киноцентр 'Салют'",
        geo_url="https://maps.google.com/salut-ekb",
        status="Идёт сейчас",
        price_min=800,
        price_max=2500,
        organizer=organizers[10]
    )
)


@dataclasses.dataclass
class Studio:
    title: str
    email: str
    content: str
    guid: typing.Optional[str] = None


studios = (
    # Россия
    Studio(title='Gaijin Entertainment', email='gaijin@mail.com', content='Разработчики War Thunder, Crossout'),
    Studio(title='1C Company', email='1c@mail.com', content='Издатели IL-2 Sturmovik, King\'s Bounty'),
    Studio(title='Saber Interactive', email='saber@mail.com', content='World War Z, SnowRunner'),
    Studio(title='Леста', email='lesta@mail.com', content='World of Tanks Blitz (ранее)'),
    Studio(title='Battlestate Games', email='battlestate@mail.com', content='Escape from Tarkov'),
    Studio(title='Ice-Pick Lodge', email='icepick@mail.com', content='Pathologic, The Void'),
    Studio(title='Mundfish', email='mundfish@mail.com', content='Atomic Heart'),
    Studio(title='Katauri Interactive', email='katauri@mail.com', content='Space Rangers, Royal Quest'),
    Studio(title='Allods Team', email='allods@mail.com', content='Allods Online, Skyforge'),
    Studio(title='Nival', email='nival@mail.com', content='Blitzkrieg, Prime World'),
    Studio(title='Cyberia Nova', email='cyberia@mail.com', content='CONSCRIPT (retro survival horror)'),
    
    # Беларусь
    Studio(title='Belka Games', email='belka@mail.com', content='Clockmaker, Seekers Notes'),
    Studio(title='Melesta Games', email='melesta@mail.com', content='Perimeter, The Wild Eight'),
    
    # Казахстан
    Studio(title='Weappy Studio', email='weappy@mail.com', content='This Is the Police'),
    Studio(title='Satur Entertainment', email='satur@mail.com', content='The Perfect Tower'),
    
    # Другие страны СНГ
    Studio(title='Azerion (Азербайджан)', email='azerion@mail.com', content='Мобильные и браузерные игры'),
    Studio(title='Somnia Labs (Армения)', email='somnia@mail.com', content='VR-игры'),
)


@dataclasses.dataclass
class Game:
    russian_title: str
    english_title: str
    alternative_title: str
    short_description: str
    link_site: str
    publisher: str
    release_date: str  # формат: "YYYY-MM-DD"
    country_development: str
    link_page_store: str
    platform: str
    studio: str
    content: str
    image: str = 'no'
    image_type: str = 'no'
    guid: typing.Optional[str] = None


games = (
    Game(
        russian_title="Атомное Сердце",
        english_title="Atomic Heart",
        alternative_title="None",
        short_description="Фантастический боевик в альтернативной реальности.",
        link_site="https://atomicheartgame.com", 
        publisher="Mundfish",
        release_date="2023-02-21",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/1063520/Atomic_Heart/", 
        platform="PC, PS4, Xbox One",
        studio=random.choice(studios),
        content="Atomic Heart — это экшен от первого лица в жанре научной фантастики."
    ),
    Game(
        russian_title="Побег из Таркова",
        english_title="Escape from Tarkov",
        alternative_title="None",
        short_description="MMOFPS с элементами RPG и выживания.",
        link_site="https://tarkov.game", 
        publisher="Battlestate Games",
        release_date="2020-08-14",
        country_development="Россия",
        link_page_store="https://tarkov.game/buy/", 
        platform="PC",
        studio=random.choice(studios),
        content="Игра про выживание в городе, наполненном опасностями и награбленным богатством."
    ),
    Game(
        russian_title="World of Tanks",
        english_title="World of Tanks",
        alternative_title="Wot",
        short_description="Массовые онлайн-танковые сражения.",
        link_site="https://worldoftanks.com", 
        publisher="Wargaming",
        release_date="2010-04-15",
        country_development="Беларусь",
        link_page_store="https://play.google.com/store/apps/details?id=com.wargaming.wot.blitz",
        platform="PC, Mobile",
        studio=random.choice(studios),
        content="Симулятор танковых баталий с поддержкой PvP и исторических режимов."
    ),
    Game(
        russian_title="Pathologic 2",
        english_title="Pathologic 2",
        alternative_title="None",
        short_description="Психологический хоррор в карантинном городе.",
        link_site="https://ice-pick.com/pathologic-2/",
        publisher="Ice-Pick Lodge",
        release_date="2019-07-23",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/632470/Pathologic_2/",
        platform="PC",
        studio=random.choice(studios),
        content="Перезапуск культовой игры с новой графикой и игровым дизайном."
    ),
    Game(
        russian_title="Conscript",
        english_title="Conscript",
        alternative_title="None",
        short_description="Ретро-хоррор о Первой мировой войне.",
        link_site="https://cyberianova.com",
        publisher="Cyberia Nova",
        release_date="2022-03-11",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/1373610/Conscript/",
        platform="PC",
        studio=random.choice(studios),
        content="Ужасы окопов времен Первой мировой войны в стиле survival horror."
    ),
    Game(
        russian_title="SnowRunner",
        english_title="SnowRunner",
        alternative_title="e",
        short_description="Экстремальные гонки по бездорожью.",
        link_site="https://www.snowrunner-game.com",
        publisher="Saber Interactive",
        release_date="2021-04-28",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/655720/SnowRunner/",
        platform="PC, PS4, Xbox One",
        studio=random.choice(studios),
        content="Продолжение MudRunner с улучшенными графикой и сеттингами."
    ),
    Game(
        russian_title="This Is the Police",
        english_title="This Is the Police",
        alternative_title="TITP",
        short_description="Драматическая стратегия о полиции маленького города.",
        link_site="https://weappystudio.com",
        publisher="Weappy Studio",
        release_date="2017-06-06",
        country_development="Казахстан",
        link_page_store="https://store.steampowered.com/app/454410/This_Is_the_Police/",
        platform="PC",
        studio=random.choice(studios),
        content="История стареющего шефа полиции, пытающегося выжить в коррумпированном городе."
    ),
    Game(
        russian_title="King's Bounty II",
        english_title="King's Bounty II",
        alternative_title="None",
        short_description="Фэнтезийная пошаговая RPG с тактическими боями.",
        link_site="https://kb2.1c.ru",
        publisher="1C Company",
        release_date="2020-08-24",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/1006060/Kings_Bounty_II/",
        platform="PC",
        studio=random.choice(studios),
        content="Обновлённая серия легендарной RPG с улучшенной системой выбора и последствий."
    ),
    Game(
        russian_title="Crossout",
        english_title="Crossout",
        alternative_title="None",
        short_description="Постапокалиптический MMO-боевик с кастомизацией машин.",
        link_site="https://crossout.net",
        publisher="Gaijin Entertainment",
        release_date="2017-05-30",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/285980/Crossout/",
        platform="PC",
        studio=random.choice(studios),
        content="Игра про битвы на самодельных машинах в мире после глобальной катастрофы."
    ),
    Game(
        russian_title="The Void",
        english_title="The Void",
        alternative_title="None",
        short_description="Абстрактная головоломка с необычной графикой.",
        link_site="https://ice-pick.com/the-void/",
        publisher="Ice-Pick Lodge",
        release_date="2009-01-22",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/190/The_Void/",
        platform="PC",
        studio=random.choice(studios),
        content="Медитативная игра с философским сюжетом и монохромной графикой."
    ),

    # Дополнительные 10 игр 

    Game(
        russian_title="War Thunder",
        english_title="War Thunder",
        alternative_title="WT",
        short_description="Многопользовательский симулятор боевых машин и авиации.",
        link_site="https://warthunder.com", 
        publisher="Gaijin Entertainment",
        release_date="2012-08-16",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/231090/War_Thunder/", 
        platform="PC, PS4, Xbox One",
        studio=random.choice(studios),
        content="Онлайн-битвы на танках, самолётах и кораблях со свободной историей развития техники."
    ),
    Game(
        russian_title="Allods Online",
        english_title="Allods Online",
        alternative_title="AION: Legacy of Ashes",
        short_description="Массовая многопользовательская ролевая онлайн-игра.",
        link_site="https://allods.mail.ru", 
        publisher="Mail.Ru / My.com",
        release_date="2009-09-22",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/466920/Allods_Online/", 
        platform="PC",
        studio=random.choice(studios),
        content="Игра в жанре MMORPG с полётом на альбах и PvP-битвами в небе."
    ),
    Game(
        russian_title="World War Z",
        english_title="World War Z",
        alternative_title="None",
        short_description="Кооперативный зомби-шутер от третьего лица.",
        link_site="https://www.worldwarz.com", 
        publisher="Saber Interactive",
        release_date="2019-04-16",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/669550/World_War_Z/", 
        platform="PC, PS4, Xbox One",
        studio=random.choice(studios),
        content="Интенсивный шутер с волнами зомби и командной тактикой."
    ),
    Game(
        russian_title="Seekers Notes",
        english_title="Seekers Notes",
        alternative_title="None",
        short_description="Квест-игра с поиском предметов и развитием деревни.",
        link_site="https://belkagames.com", 
        publisher="Belka Games",
        release_date="2012-11-22",
        country_development="Беларусь",
        link_page_store="https://play.google.com/store/apps/details?id=com.belka.seeker",
        platform="Mobile",
        studio=random.choice(studios),
        content="Мобильная головоломка с красивыми локациями и детективной составляющей."
    ),
    Game(
        russian_title="Perimeter",
        english_title="Perimeter",
        alternative_title="None",
        short_description="RTS с уникальным геймплеем и энергетическими щитами.",
        link_site="https://melesta-games.com",
        publisher="Melesta Games",
        release_date="2004-06-01",
        country_development="Беларусь",
        link_page_store="https://store.steampowered.com/app/226300/Perimeter_Strategic_Commander/",
        platform="PC",
        studio=random.choice(studios),
        content="Стратегия с защитными барьерами, где важна не только атака, но и оборона."
    ),
    Game(
        russian_title="Clockmaker",
        english_title="Clockmaker",
        alternative_title="None",
        short_description="Хоррор-квест с психологическим напряжением.",
        link_site="https://belkagames.com",
        publisher="Belka Games",
        release_date="2014-10-24",
        country_development="Беларусь",
        link_page_store="https://store.steampowered.com/app/309540/Clockmaker/",
        platform="PC",
        studio=random.choice(studios),
        content="История о человеке, который создаёт механические копии людей."
    ),
    Game(
        russian_title="Space Rangers 2",
        english_title="Space Rangers 2",
        alternative_title="None",
        short_description="Космическая RPG с элементами стратегии и приключений.",
        link_site="https://katauri.ru",
        publisher="Katauri Interactive",
        release_date="2004-11-19",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/226300/Space_Rangers_2/",
        platform="PC",
        studio=random.choice(studios),
        content="Звёздные рейнджеры продолжают свои приключения в далёком будущем."
    ),
    Game(
        russian_title="Royal Quest",
        english_title="Royal Quest",
        alternative_title="None",
        short_description="MMORPG в средневековом фэнтезийном мире.",
        link_site="https://katauri.ru",
        publisher="Katauri Interactive",
        release_date="2006-09-22",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/226300/Royal_Quest/",
        platform="PC",
        studio=random.choice(studios),
        content="Игра с классической механикой, рыцарскими турнирами и магией."
    ),
    Game(
        russian_title="Blitzkrieg",
        english_title="Blitzkrieg",
        alternative_title="None",
        short_description="Реалистичная тактическая стратегия Великой Отечественной войны.",
        link_site="https://nival.com",
        publisher="Nival",
        release_date="2002-09-17",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/226300/Blitzkrieg/",
        platform="PC",
        studio=random.choice(studios),
        content="Стратегия с акцентом на реализм и историческую достоверность военных действий."
    ),
    Game(
        russian_title="Prime World",
        english_title="Prime World",
        alternative_title="None",
        short_description="MOBA с элементами стратегии и магического мира.",
        link_site="https://prime-world.com",
        publisher="Nival",
        release_date="2014-04-22",
        country_development="Россия",
        link_page_store="https://store.steampowered.com/app/226300/Prime_World/",
        platform="PC",
        studio=random.choice(studios),
        content="Командная MOBA с уникальными героями и заклинаниями."
    ),
)


@dataclasses.dataclass
class Genre:
    title: str
    description: str
    games_genres: list[Game]
    guid: typing.Optional[str] = None


genres = (
    Genre(
        title='Экшен от первого лица',
        description='Игра с видом от первого лица, сосредоточенная на динамичных боевых действиях.',
        games_genres=[games[0], games[1], games[11]]  # Atomic Heart, Escape from Tarkov, World War Z
    ),
    Genre(
        title='MMOFPS',
        description='Массовые многопользовательские шутеры от первого лица с PvP-битвами.',
        games_genres=[games[1], games[10]]  # Escape from Tarkov, War Thunder
    ),
    Genre(
        title='Танковый симулятор',
        description='Реалистичные сражения на исторической и вымышленной бронетехнике.',
        games_genres=[games[2], games[10]]  # World of Tanks, War Thunder
    ),
    Genre(
        title='Психологический хоррор',
        description='Игры с напряженной атмосферой и акцентом на эмоциональном восприятии.',
        games_genres=[games[3], games[15]]  # Pathologic 2, Clockmaker
    ),
    Genre(
        title='Survival Horror',
        description='Выживание в условиях постоянной опасности и ограниченных ресурсах.',
        games_genres=[games[4], games[11]]  # Conscript, World War Z
    ),
    Genre(
        title='Гонки / Симулятор бездорожья',
        description='Игры про управление внедорожниками в сложных условиях.',
        games_genres=[games[5]]  # SnowRunner
    ),
    Genre(
        title='Стратегия (TBS)',
        description='Пошаговая тактическая стратегия с развитием персонажей и выбором решений.',
        games_genres=[games[7], games[19]]  # King's Bounty II, Space Rangers 2
    ),
    Genre(
        title='Квест / Поиск предметов',
        description='Игры, основанные на поиске скрытых объектов и решении головоломок.',
        games_genres=[games[13], games[14]]  # Seekers Notes, Clockmaker
    ),
    Genre(
        title='Постапокалипсис',
        description='Действие разворачивается в мире после глобальной катастрофы.',
        games_genres=[games[8], games[1]]  # Crossout, Escape from Tarkov
    ),
    Genre(
        title='Философская головоломка',
        description='Игры с абстрактным геймплеем и глубоким смыслом.',
        games_genres=[games[9], games[3]]  # The Void, Pathologic 2
    ),
    Genre(
        title='RTS / Тактическая стратегия',
        description='Реальное время, управление армией и тактические решения.',
        games_genres=[games[16]]  # Perimeter, Blitzkrieg
    ),
    Genre(
        title='Фэнтезийная RPG',
        description='Игры в мире магии, рыцарей и древних рас.',
        games_genres=[games[7], games[18]]  # King's Bounty II, Royal Quest
    ),
    Genre(
        title='Историческая стратегия',
        description='Основана на реальных исторических событиях и военных действиях.',
        games_genres=[games[16]]  # Perimeter, Blitzkrieg
    ),
    Genre(
        title='Казуальная головоломка',
        description='Простые в освоении игры для коротких сессий.',
        games_genres=[games[13], games[14]]  # Seekers Notes, Clockmaker
    ),
    Genre(
        title='Симулятор управления',
        description='Имитация реального процесса управления техникой или системами.',
        games_genres=[games[5], games[10]]  # SnowRunner, War Thunder
    ),
    Genre(
        title='Открытый мир',
        description='Широкое игровое пространство с возможностью свободного перемещения.',
        games_genres=[games[0], games[1], games[8]]  # Atomic Heart, Escape from Tarkov, Crossout
    ),
    Genre(
        title='Кооперативный шутер',
        description='Командная игра с упором на совместное прохождение.',
        games_genres=[games[11], games[8]]  # World War Z, Crossout
    ),
    Genre(
        title='Драматическая стратегия',
        description='Игра с сильным сюжетом и моральными дилеммами.',
        games_genres=[games[6], games[3]]  # This Is the Police, Pathologic 2
    ),
)


@dataclasses.dataclass
class Tag:
    title: str
    games: list[Game]
    guid: typing.Optional[str] = None


tags = (
    Tag(
        title="Россия",
        games=[game for game in games if game.country_development == "Россия"]
    ),
    Tag(
        title="Беларусь",
        games=[game for game in games if game.country_development == "Беларусь"]
    ),
    Tag(
        title="Казахстан",
        games=[game for game in games if game.country_development == "Казахстан"]
    ),
    Tag(
        title="PC",
        games=[game for game in games if "PC" in game.platform]
    ),
    Tag(
        title="PS4",
        games=[game for game in games if "PS4" in game.platform]
    ),
    Tag(
        title="Xbox One",
        games=[game for game in games if "Xbox One" in game.platform]
    ),
    Tag(
        title="Mobile",
        games=[game for game in games if "Mobile" in game.platform]
    ),
    Tag(
        title="Survival",
        games=[game for game in games if "выживание" in game.short_description.lower() or "survival" in game.content.lower()]
    ),
    Tag(
        title="Хоррор",
        games=[game for game in games if "хоррор" in game.short_description.lower() or "ужас" in game.content.lower()]
    ),
    Tag(
        title="Онлайн",
        games=[game for game in games if "онлайн" in game.short_description.lower() or "онлайн" in game.content.lower()]
    ),
    Tag(
        title="Шутер",
        games=[game for game in games if "шутер" in game.short_description.lower() or "шутер" in game.content.lower()]
    ),
    Tag(
        title="Фэнтези",
        games=[game for game in games if "фэнтез" in game.short_description.lower() or "маг" in game.content.lower()]
    ),
    Tag(
        title="История",
        games=[game for game in games if "истори" in game.content.lower()]
    ),
    Tag(
        title="MMO",
        games=[game for game in games if "mmo" in game.short_description.lower()]
    ),
)


def gen_organizers(**kw) -> typing.Iterator[tuple[str, Organizer, dict]]:
    addr = kw['addr'] + '/Organizer/Create'
    creator_id = kw['usr_id']
    owner_id = kw['usr_id']
    for obj in organizers:
        yield (
            addr,
            obj,
            {
                'title': obj.title,
                'mail': obj.mail,
                'content': obj.content,
                'isPublicationAllowed': True,
                'creatorId': creator_id,
                'ownerId': owner_id,
                'image': 'no',
                'imageType': 'no',
            }
        )


def gen_events(**kw) -> typing.Iterator[tuple[str, Event, dict]]:
    addr = f'{kw["addr"]}/Event/Create'
    creator_id = kw['usr_id']
    owner_id = kw['usr_id']
    for obj in events:
        yield (
            addr,
            obj,
            {
                'title': obj.title,
                'country': obj.country,
                'region': obj.region,
                'city': obj.city,
                'address': obj.address,
                'geoUrl': obj.geo_url,
                'status': obj.status,
                'priceMin': obj.price_min,
                'priceMax': obj.price_max,
                'organizerEventId': obj.organizer.guid,
                'isPublicationAllowed': True,
                'creatorId': creator_id,
                'ownerId': owner_id,
                'image': 'no',
                'imageType': 'no',
                'content': 'Пусто',
            }
        )


def gen_studios(**kw) -> typing.Iterator[tuple[str, Studio, dict]]:
    addr = kw['addr'] + '/Studio/CreateStudio'
    creator_id = kw['usr_id']
    owner_id = kw['usr_id']
    for obj in studios:
        yield (
            addr,
            obj,
            {
                'title': obj.title,
                'email': obj.email,
                'content': obj.content,
                'image': 'no',
                'imageType': 'no',
            }
        )


def gen_games(**kw) -> typing.Iterator[tuple[str, Game, dict]]:
    addr = kw['addr'] + '/Game/CreateGame'
    creator_id = kw['usr_id']
    owner_id = kw['usr_id']
    for obj in games:
        yield (
            addr,
            obj,
            {
                "russianTitle": obj.russian_title,
                "englishTitle": obj.english_title,
                "alternativeTitle": obj.alternative_title,
                "shortDescription": obj.short_description,
                "linkSite": obj.link_site,
                "publisher": obj.publisher,
                "releaseDate": obj.release_date,
                "countryDevelopment": obj.country_development,
                "linkPageStore": obj.link_page_store,
                "platform": obj.platform,
                "studioId": obj.studio.guid,
                "image": obj.image,
                "imageType": obj.image_type,
                "content": obj.content,
                "isPublicationAllowed": True,
                "creatorId": creator_id,
                "ownerId": owner_id,
            }
        )


def gen_games(**kw) -> typing.Iterator[tuple[str, Game, dict]]:
    addr = kw['addr'] + '/Game/CreateGame'
    creator_id = kw['usr_id']
    owner_id = kw['usr_id']
    for obj in games:
        yield (
            addr,
            obj,
            {
                "russianTitle": obj.russian_title,
                "englishTitle": obj.english_title,
                "alternativeTitle": obj.alternative_title,
                "shortDescription": obj.short_description,
                "linkSite": obj.link_site,
                "publisher": obj.publisher,
                "releaseDate": obj.release_date,
                "countryDevelopment": obj.country_development,
                "linkPageStore": obj.link_page_store,
                "platform": obj.platform,
                "studioId": obj.studio.guid,
                "image": obj.image,
                "imageType": obj.image_type,
                "content": obj.content,
                "isPublicationAllowed": True,
                "creatorId": creator_id,
                "ownerId": owner_id,
            }
        )


def gen_genres(**kw) -> typing.Iterator[tuple[str, Genre, dict]]:
    addr = kw['addr'] + '/Genre/CreateGenre'
    creator_id = kw['usr_id']
    owner_id = kw['usr_id']
    for obj in genres:
        yield (
            addr,
            obj,
            {
                "title": obj.title,
                "description": obj.description,
            }
        )


def gen_games_genres(**kw) -> typing.Iterator[tuple[str, Genre, dict]]:
    addr = kw['addr'] + '/GameSelectedGenre/CreateGameSelectedGenre'
    creator_id = kw['usr_id']
    owner_id = kw['usr_id']
    for obj in genres:
        for game in obj.games_genres:
            yield (
                addr,
                obj,
                {
                    "numberOrder": 1,
                    "gameId": game.guid,
                    "genreId": obj.guid,
                }
            )


def gen_tags(**kw) -> typing.Iterator[tuple[str, Tag, dict]]:
    addr = kw['addr'] + '/Tag/CreateTag'
    creator_id = kw['usr_id']
    owner_id = kw['usr_id']
    for obj in tags:
        yield (
            addr,
            obj,
            {
                "title": obj.title,
            }
        )


def gen_game_tags(**kw) -> typing.Iterator[tuple[str, Tag, dict]]:
    addr = kw['addr'] + '/GameSelectedTag/CreateGameSelectedTag'
    creator_id = kw['usr_id']
    owner_id = kw['usr_id']
    for obj in tags:
        for game in obj.games:
            yield (
                addr,
                obj,
                {
                    "gameId": game.guid,
                    "tagId": obj.guid,
                }
            )


async def main():
    await send_requests(gen_organizers)
    await send_requests(gen_events)
    await send_requests(gen_studios)
    await send_requests(gen_games)
    await send_requests(gen_genres)
    await send_requests(gen_games_genres)
    await send_requests(gen_tags)
    await send_requests(gen_game_tags)


async def send_requests(*gens):
    addr = "https://localhost:7060/api"
    usr_id = "019797f1-e6e9-7213-aa9f-65e7bd4c62eb"
    coros = []
    for get in gens:
        for addr, obj, data in get(addr=addr, usr_id=usr_id):
            coro = send_request(addr, obj, data)
            coros.append(coro)
    await asyncio.gather(*coros)


async def send_request(addr: str, obj, data: dict):
    token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIwMTk3OTdmMS1lNmU5LTcyMTMtYWE5Zi02NWU3YmQ0YzYyZWIiLCJ1c2VyVGVsZWdyYW1JZCI6IjE4MjM1OTE4MjUiLCJqdGkiOiJhZDRhODllMS0yNTVjLTRhYzktOTc1ZC04NmVkZTIzNzIxZTkiLCJyb2xlIjoidXNlciIsIm5iZiI6MTc1MDYwMDkwMywiZXhwIjoxNzUwNjg3MzAzLCJpYXQiOjE3NTA2MDA5MDMsImlzcyI6ImdldC1hd2FpdC1zZXJ2aWNlIiwiYXVkIjoiZ2V0LWF3YWl0LXNlcnZpY2UifQ.qCsGHacxCSKZ0eoNtxL8WxqAd0T2PQwhJLnS3Fx3ZASQVLWlRFaEa04R7jVJMOgAnv44iQPRte99RqF2CuwCKw"
    headers = {
        "Authorization": f"Bearer {token}",
        "Content-Type": "application/json",
        "Accept": "*/*"
    }
    ssl_context = False
    async with aiohttp.ClientSession() as session:
        async with session.post(addr, json=data, ssl_context=ssl_context, headers=headers) as resp:
            if 200 <= resp.status < 300:
                json = await resp.json()
                obj.guid = json['id']
            pprint(data)
            print(resp.status)
            print(resp.text)


if __name__ == '__main__':
    asyncio.run(main())
