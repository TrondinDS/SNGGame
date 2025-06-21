import asyncio
import dataclasses
import typing

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
    Organizer(title='Wargaming', mail='wargaming@mail.com', content='World of Tanks, World of Warships'),
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
    
    # Wargaming (Беларусь)
    Event(
        title="Мировой финал World of Tanks",
        country="Беларусь",
        region="Минск",
        city="Минск",
        address="Минск-Арена",
        geo_url="https://maps.google.com/minsk-arena",
        status="Предстоящее",
        price_min=1500,
        price_max=20000,
        organizer=organizers[11]
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


async def main():
    addr = "https://localhost:7060"
    usr_id = "0000-0000-0000"
    coros = []
    for get in (gen_organizers, gen_events):
        for addr, obj, data in get(addr=addr, usr_id=usr_id):
            coro = send_request(addr, obj, data)
            coros.append(coro)
    await asyncio.gather(*coros)


async def send_request(addr: str, obj, data: dict):
    async with aiohttp.ClientSession() as session:
        async with session.post(addr, json=data) as resp:
            # TODO pass guid to obj: obj.guid = await resp.json()['guid']
            return await resp.text()


def gen_organizers(**kw) -> typing.Iterator[tuple[str, Organizer, dict]]:
    addr = f'{kw["addr"]}/Organizer/Create'
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
                'organizerId': obj.organizer.guid,
                'isPublicationAllowed': True,
                'creatorId': creator_id,
                'ownerId': owner_id,
            }
        )


if __name__ == '__main__':
    asyncio.run(main())
