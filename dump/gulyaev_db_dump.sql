--
-- PostgreSQL database dump
--

\restrict TUxOhcDrJCU55ifSL031ZYSNtnfXbQnlH6fnmMAA7bmiAVpvzQS3XTXcQbjh1VY

-- Dumped from database version 17.6
-- Dumped by pg_dump version 17.6

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: app; Type: SCHEMA; Schema: -; Owner: app
--

CREATE SCHEMA app;


ALTER SCHEMA app OWNER TO app;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: partner_products; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.partner_products (
    id integer NOT NULL,
    product integer,
    partner integer,
    partner_count integer,
    sale_date date
);


ALTER TABLE app.partner_products OWNER TO app;

--
-- Name: partner_products_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.partner_products_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE app.partner_products_id_seq OWNER TO app;

--
-- Name: partner_products_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.partner_products_id_seq OWNED BY app.partner_products.id;


--
-- Name: partners; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.partners (
    id integer NOT NULL,
    partner_type character varying(5),
    partner_name character varying(50),
    partner_director character varying(120),
    partner_email character varying(120),
    partner_number character varying(12),
    partner_address character varying(240),
    partner_inn character varying,
    partner_rating smallint
);


ALTER TABLE app.partners OWNER TO app;

--
-- Name: partners_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.partners_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE app.partners_id_seq OWNER TO app;

--
-- Name: partners_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.partners_id_seq OWNED BY app.partners.id;


--
-- Name: product_type; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.product_type (
    id integer NOT NULL,
    product_type character varying(20),
    coaf_product_type real
);


ALTER TABLE app.product_type OWNER TO app;

--
-- Name: product_type_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.product_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE app.product_type_id_seq OWNER TO app;

--
-- Name: product_type_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.product_type_id_seq OWNED BY app.product_type.id;


--
-- Name: products; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.products (
    id integer NOT NULL,
    product_type integer,
    product_name character varying(120),
    partner_artc integer,
    partner_price real
);


ALTER TABLE app.products OWNER TO app;

--
-- Name: products_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.products_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE app.products_id_seq OWNER TO app;

--
-- Name: products_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.products_id_seq OWNED BY app.products.id;


--
-- Name: partner_products id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.partner_products ALTER COLUMN id SET DEFAULT nextval('app.partner_products_id_seq'::regclass);


--
-- Name: partners id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.partners ALTER COLUMN id SET DEFAULT nextval('app.partners_id_seq'::regclass);


--
-- Name: product_type id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.product_type ALTER COLUMN id SET DEFAULT nextval('app.product_type_id_seq'::regclass);


--
-- Name: products id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products ALTER COLUMN id SET DEFAULT nextval('app.products_id_seq'::regclass);


--
-- Data for Name: partner_products; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.partner_products (id, product, partner, partner_count, sale_date) FROM stdin;
4	3	1	123	2026-03-10
7	3	1	10000	2026-03-17
\.


--
-- Data for Name: partners; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.partners (id, partner_type, partner_name, partner_director, partner_email, partner_number, partner_address, partner_inn, partner_rating) FROM stdin;
1	ЗАО	Тест	Тестов тест тестович	sdfgdf@mail.ru	+79504563234	Г.Пермь	123333333331	4
12	ОАО	Тест2	Директор	test@test.ru	+79543233212	sfd	1234567854	2
\.


--
-- Data for Name: product_type; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.product_type (id, product_type, coaf_product_type) FROM stdin;
1	Ламинат	2.35
2	Массивная доска	5.15
3	Паркетная доска	4.34
4	Пробковое покрытие	1.5
\.


--
-- Data for Name: products; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.products (id, product_type, product_name, partner_artc, partner_price) FROM stdin;
1	3	Паркетная доска Ясень темный однополосная 14 мм	8758385	4456.9
2	3	Инженерная доска Дуб Французская елка однополосная 12 мм	8858958	7330.99
3	1	Ламинат Дуб дымчато-белый 33 класс 12 мм	7750282	1799.33
4	1	Ламинат Дуб серый 32 класс 8 мм с фаской	7028748	3890.41
5	4	Пробковое напольное клеевое покрытие 32 класс 4 мм	5012543	5450.59
\.


--
-- Name: partner_products_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.partner_products_id_seq', 11, true);


--
-- Name: partners_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.partners_id_seq', 12, true);


--
-- Name: product_type_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.product_type_id_seq', 4, true);


--
-- Name: products_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.products_id_seq', 10, true);


--
-- Name: partner_products partner_products_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.partner_products
    ADD CONSTRAINT partner_products_pkey PRIMARY KEY (id);


--
-- Name: partners partners_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.partners
    ADD CONSTRAINT partners_pkey PRIMARY KEY (id);


--
-- Name: product_type product_type_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.product_type
    ADD CONSTRAINT product_type_pkey PRIMARY KEY (id);


--
-- Name: products products_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (id);


--
-- Name: partner_products partner_products_partner_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.partner_products
    ADD CONSTRAINT partner_products_partner_fkey FOREIGN KEY (partner) REFERENCES app.partners(id);


--
-- Name: partner_products partner_products_product_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.partner_products
    ADD CONSTRAINT partner_products_product_fkey FOREIGN KEY (product) REFERENCES app.products(id);


--
-- Name: products products_product_type_fkey; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products
    ADD CONSTRAINT products_product_type_fkey FOREIGN KEY (product_type) REFERENCES app.product_type(id);


--
-- PostgreSQL database dump complete
--

\unrestrict TUxOhcDrJCU55ifSL031ZYSNtnfXbQnlH6fnmMAA7bmiAVpvzQS3XTXcQbjh1VY

