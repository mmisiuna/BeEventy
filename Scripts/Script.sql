DROP TYPE IF EXISTS "statistics" CASCADE;
DROP TYPE IF EXISTS "weapon_type" CASCADE;
DROP TYPE IF EXISTS "status" CASCADE;
DROP TYPE IF EXISTS "enchantment" CASCADE;

DROP TABLE IF EXISTS "user" CASCADE;
DROP TABLE IF EXISTS "race" CASCADE;
DROP TABLE IF EXISTS "sympathy_bonus" CASCADE;
DROP TABLE IF EXISTS "quest" CASCADE;
DROP TABLE IF EXISTS "attribute" CASCADE;
DROP TABLE IF EXISTS "region" CASCADE;
DROP TABLE IF EXISTS "body_part" CASCADE;
DROP TABLE IF EXISTS "armor" CASCADE;
DROP TABLE IF EXISTS "item" CASCADE;

DROP TABLE IF EXISTS "npc" CASCADE;
DROP TABLE IF EXISTS "npc_location" CASCADE;
DROP TABLE IF EXISTS "location" CASCADE;

DROP TABLE IF EXISTS "classes" CASCADE;
DROP TABLE IF EXISTS "classes_skills" CASCADE;
DROP TABLE IF EXISTS "skills" CASCADE;


CREATE TYPE "statistics" as  (
  strength INTEGER,
  dexterity INTEGER,
  intelligence INTEGER,
  stamina INTEGER,
  luck INTEGER,
  speed INTEGER
);

CREATE TYPE weapon_type AS ENUM ('player', 'enemy', 'fabular');

CREATE TYPE status AS enum (
  'sleep',
  'poisoned',
  'burning',
  'bleeding',
  'blind'
);

CREATE TYPE role AS enum (
  'player',
  'enemy',
  'fabular'
);

CREATE TYPE enchantment AS ENUM ('fire_aspect', 'electric_aspect', 'etc');

CREATE TABLE "location" (
  id SERIAL PRIMARY KEY,
  name VARCHAR
  -- region_id integer [ref: > region.id]
);

CREATE TABLE "item" (
  id SERIAL PRIMARY KEY,
  name VARCHAR,
  quantity INTEGER,
  is_usable BOOLEAN
  additional_statistics statistics,
  enchantment enchantment
  
--  player_id integer [ref: <> classes.id]
--  class_consistency_ids integer[]
--  region_id integer  [ref: <> region.id]
);

CREATE TABLE "armor" (
  id SERIAL PRIMARY KEY,
  item_id INTEGER REFERENCES "item"(id), --item_id integer [ref: < item.id]
  armor_points INTEGER,
  body_part_id INTEGER REFERENCES "body_part"(id) -- body_part_id integer [ref: > body_part.id]
);

CREATE TABLE "body_part" (
  id SERIAL PRIMARY KEY,
  name VARCHAR,
  is_busy BOOLEAN
);

CREATE TABLE "npc" (
  id SERIAL PRIMARY KEY,
  name VARCHAR,
  portrait VARCHAR,
  sympathy_level INTEGER
);

CREATE TABLE "npc_location" ( --location_id integer [ref: <> location.id]
  npc_id INTEGER REFERENCES "npc"(id) ON UPDATE CASCADE ON DELETE CASCADE,
  location_id INTEGER REFERENCES "location"(id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE "sympathy_bonus" (
  id SERIAL PRIMARY KEY,
  name VARCHAR,
  description VARCHAR,
  npc_id INTEGER REFERENCES "npc"(id) --[ref: < npc.id]
);

CREATE TABLE "quest" (
  id SERIAL PRIMARY KEY,
  name VARCHAR,
  story VARCHAR,
  energy_cost INTEGER,
  experience_reward INTEGER,
  gold_reward INTEGER,
  item_reward_id INTEGER REFERENCES "item"(id),
  npc_id INTEGER REFERENCES "npc"(id)
);

CREATE TABLE "user" (
  id SERIAL PRIMARY KEY,
  nick VARCHAR,
  email VARCHAR,
  "password" VARCHAR,
  is_active BOOLEAN,
  date_of_created DATE,
  party INTEGER REFERENCES character(id),
  active_sympathy_bonus_id INTEGER REFERENCES "sympathy_bonus"(id),	-- [ref: < sympathy_bonus.id]
  gold INTEGER,
  magic_dust INTEGER,
  enable_characters_id REFERENCES character(id)
);

CREATE TABLE "race" (
  id SERIAL PRIMARY KEY,
  icon VARCHAR,
  description VARCHAR,
  level_up_statistics statistics
);

CREATE TABLE "classes" (
  id SERIAL PRIMARY KEY,
  name VARCHAR,
  icon VARCHAR,
  description VARCHAR
);

CREATE TABLE "skills" (
  id SERIAL PRIMARY KEY,
  name VARCHAR,
  icon VARCHAR,
  description VARCHAR
);

CREATE TABLE "classes_skills" (
  classes_id INTEGER REFERENCES "classes"(id) ON UPDATE CASCADE ON DELETE CASCADE,
  skills_id INTEGER REFERENCES "skills"(id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE "attribute" (
  id SERIAL PRIMARY KEY,
  name VARCHAR,
  can_be_heal BOOLEAN,
  resist_status status,
  physical_damage_taken_scaling DECIMAL,
  magical_damage_taken_scaling DECIMAL
);

CREATE TABLE "region" (
  id SERIAL PRIMARY KEY,
  name VARCHAR
);



